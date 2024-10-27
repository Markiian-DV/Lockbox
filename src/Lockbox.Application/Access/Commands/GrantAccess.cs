using Lockbox.Application.Contracts;
using Lockbox.Application.Cryptography;
using Lockbox.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lockbox.Application.Access.Commands;

public record GrantAccessCommand(string OwnerId, string TargetUserEmail, string FileId, AccessLevel AccessLevel, string PrivateKey) : IRequest;

public class GrantAccessCommandHandler : IRequestHandler<GrantAccessCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IUserService _userService;

    public GrantAccessCommandHandler(IApplicationDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public async Task Handle(GrantAccessCommand request, CancellationToken ct)
    {
        var targetUser = await _userService.GetUserByEmail(request.TargetUserEmail);
        var tuPublicKey = await _dbContext.PublicKeys.FirstOrDefaultAsync(k => k.UserId == targetUser.Id);
        var file = (await _dbContext.Files.FirstOrDefaultAsync(f => f.Id == new Guid(request.FileId))) ?? throw new Exception("File does not exist");
        var tuFileAccess = await _dbContext.FilesAccess
            .FirstOrDefaultAsync(fa => fa.UserId == targetUser.Id && fa.FileId == file.Id && fa.RevokedData == null);
        
        if(file.OwnerId != request.OwnerId)
        {
            throw new Exception("user is not owner");
        }
        if (tuFileAccess is not null)
        {
            throw new Exception("user already have access to the file");
        }
        
        var ouFileAccess = await _dbContext.FilesAccess
            .FirstOrDefaultAsync(fa => fa.UserId == request.OwnerId && fa.FileId == file.Id && fa.RevokedData == null);

        var fileAccessKey = RsaCryptoService.Decrypt(request.PrivateKey, ouFileAccess.EncryptedFileAccessKey);
        var tuEncryptedFileAccessKey = RsaCryptoService.Encrypt(tuPublicKey.KeyValue, fileAccessKey.ToString());
        
        await _dbContext.FilesAccess.AddAsync(new Domain.Entities.FileAccess{
            UserId = targetUser.Id,
            FileId = file.Id,
            EncryptedFileAccessKey = tuEncryptedFileAccessKey,
            AccessLevel = request.AccessLevel
        });

        await _dbContext.SaveChangesAsync(ct);
    }
}
