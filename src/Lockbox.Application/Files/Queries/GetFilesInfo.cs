using Lockbox.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lockbox.Application.Files.Queries;

public record GetFilesInfoQuery(string UserId) : IRequest<IEnumerable<FileInfo>>;

public class FileInfo
{
    public required string FileId { get; set; }
    public required string FileName { get; set; }
    public required long FileSize { get; set; }
    public required string OwnerEmail { get; set; }
    public required string AccessLevel { get; set; }
}

public class GetFilesInfoQueryHandler : IRequestHandler<GetFilesInfoQuery, IEnumerable<FileInfo>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IUserService _userService;

    public GetFilesInfoQueryHandler(IApplicationDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public async Task<IEnumerable<FileInfo>> Handle(GetFilesInfoQuery request, CancellationToken ct)
    {
        var filesInfo = await _dbContext.FilesAccess
            .Where(fa => fa.UserId == request.UserId && fa.RevokedData == null)
            .Join(_dbContext.Files, fa => fa.FileId, f => f.Id, (fa, f) => new
            {
                FileId = f.Id.ToString(),
                FileName = f.Name,
                FileSize = f.SizeInBytes,
                AccessLevel = fa.AccessLevel.ToString(),
                f.OwnerId
            }).ToListAsync();

        var userEmails = (await _userService.GetUsers(filesInfo.Select(fi => fi.OwnerId))).ToDictionary(u => u.Id, u => u.Email);

        var result = filesInfo.Select(fi => new FileInfo
        {
            FileId = fi.FileId,
            FileName = fi.FileName,
            FileSize = fi.FileSize,
            AccessLevel = fi.AccessLevel,
            OwnerEmail = userEmails[fi.OwnerId]
        }).ToList();

        return result;
    }
}