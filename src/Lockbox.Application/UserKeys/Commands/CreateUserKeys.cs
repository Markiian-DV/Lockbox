using Lockbox.Application.Contracts;
using Lockbox.Application.Cryptography;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lockbox.Application.UserKeys.Commands;

public record CreateUserKeysCommand(string UserId) : IRequest<PrivateKey>;

public record PrivateKey(string Value);

public class CreateUserKeysCommandHandler : IRequestHandler<CreateUserKeysCommand, PrivateKey>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateUserKeysCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PrivateKey> Handle(CreateUserKeysCommand request, CancellationToken ct)
    {
        var existingKey = await _dbContext.PublicKeys.FirstOrDefaultAsync(x => x.UserId == request.UserId, ct);
        if(existingKey is not null)
        {
            // #TODO: user result wrapper or custom exception.
            throw new Exception("key already exist");
        }

        var (publicKey, privateKey) = RsaCryptoService.GenerateKey();
        await _dbContext.PublicKeys.AddAsync(new Domain.Entities.PublicKey
        {
            UserId = request.UserId,
            KeyValue = publicKey
        }, ct);

        await _dbContext.SaveChangesAsync(ct);
        return new PrivateKey(privateKey);
    }
}