using Lockbox.Application.Contracts;
using Lockbox.Application.Cryptography;
using Lockbox.Domain.Converters;
using Lockbox.Domain.Enums;
using MediatR;
using File = Lockbox.Domain.Entities.File;

namespace Lockbox.Application.Files.Commands;

public record CreateFileCommand(string FileName, Stream Stream, string FileType, string UserId) : IRequest;

public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateFileCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreateFileCommand request, CancellationToken ct)
    {
        var fileAccessKey = AesCryptoService.CreateFileAccessKey();
        var fileId = Guid.NewGuid();
        // this abracadabra code encrypts and safes file
        using (var outputFileStream = new FileStream("../../LockboxData/" + fileId.ToString(), FileMode.Create, System.IO.FileAccess.Write))
        {
            await AesCryptoService.EncryptFileAsync(fileAccessKey, request.Stream, outputFileStream);
        }

        var file = new File()
        {
            Id = fileId,
            Name = request.FileName,
            SizeInBytes = request.Stream.Length,
            OwnerId = request.UserId,
            FileType = FileTypeConverters.GetFileTypeFromName(request.FileName)
        };
        await _dbContext.Files.AddAsync(file);

        var publicKey = _dbContext.PublicKeys.FirstOrDefault(k => k.UserId == request.UserId);
        var encryptedFileAccessKey = RsaCryptoService.Encrypt(publicKey.KeyValue, fileAccessKey.ToString());
        var fileAccess = new Domain.Entities.FileAccess
        {
            FileId = fileId,
            UserId = request.UserId,
            EncryptedFileAccessKey = encryptedFileAccessKey,
            AccessLevel = AccessLevel.Write
        };
        await _dbContext.FilesAccess.AddAsync(fileAccess);

        await _dbContext.SaveChangesAsync();
    }
}

// 1. користувач закидає файл `
// 2. створюються симетричний ключ для файлу. `
// 3. файл шифрується `
// 4. файл зберігається на сервері `
// 5. симетричний ключ шифрується публічним ключем юзера. `
// 6. метадані про файл та симетричний ключ зберігаєтся в бд `