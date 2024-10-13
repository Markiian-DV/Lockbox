using Lockbox.Application.Contracts;
using Lockbox.Application.Cryptography;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lockbox.Application.Files.Queries;

public record GetFileCommand(string UserId, Guid FileId, string PrivateKey) : IRequest<GetFileResult>;
// mb will add content type support
public record GetFileResult(Stream Stream, string FileName);

public class GetFileCommandHandler : IRequestHandler<GetFileCommand, GetFileResult>
{
    private readonly IApplicationDbContext _dbContext;

    public GetFileCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetFileResult> Handle(GetFileCommand request, CancellationToken cancellationToken)
    {
        var fileAccess = await _dbContext.FilesAccess
         .SingleOrDefaultAsync(e => e.FileId == request.FileId && e.UserId == request.UserId);
        var file = await _dbContext.Files.FirstOrDefaultAsync(e => e.Id == request.FileId);

        var fileAccessKey = RsaCryptoService.Decrypt(request.PrivateKey, fileAccess.EncryptedFileAccessKey);
        
        // that is total pizdec` as i read potentially large files into RAM
        var outputStream = new MemoryStream((int)file.SizeInBytes);
        using var encryptedFileStream = new FileStream("../../LockboxData/" + request.FileId.ToString(), FileMode.Open, FileAccess.Read);

        await AesCryptoService.DecryptFileAsync(new Guid(fileAccessKey), encryptedFileStream, outputStream);
        // Closing a decorator stream (CryptoStream in DecryptFileAsync) closes both the decorator and its backing store (outputStream) stream. So return new stream
        return new GetFileResult(new MemoryStream(outputStream.GetBuffer()), file.Name);
    }
}