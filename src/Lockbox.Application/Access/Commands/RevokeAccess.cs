using Lockbox.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lockbox.Application.Access.Commands;

public record RevokeAccessCommand(string OwnerId, string TargetUserEmail, string FileId) : IRequest;

public class RevokeAccessCommandHandler : IRequestHandler<RevokeAccessCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IUserService _userService;

    public RevokeAccessCommandHandler(IApplicationDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public async Task Handle(RevokeAccessCommand request, CancellationToken ct)
    {
        // TODO: validation owner != targetUser
        var file = await _dbContext.Files.FirstOrDefaultAsync(f => f.Id == new Guid(request.FileId));
        if (file is null || file.OwnerId != request.OwnerId)
        {
            throw new Exception("No access");
        }
        var targetUser = await _userService.GetUserByEmail(request.TargetUserEmail);
        var fileAccess = await _dbContext.FilesAccess
            .FirstOrDefaultAsync(f => f.FileId == file.Id && f.UserId == targetUser.Id && f.RevokedData == null);

        if(fileAccess is not null)
        {
            fileAccess.RevokedData = DateTime.UtcNow;
            _dbContext.FilesAccess.Update(fileAccess);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}

