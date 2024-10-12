namespace Lockbox.Domain.Converters;
using Lockbox.Domain.Enums;
using Lockbox.Domain.Exceptions;

public static class FileTypeConverters
{
    public static FileType GetFileTypeFromName(string name)
    {
        var extension = Path.GetExtension(name);
        if (string.IsNullOrWhiteSpace(extension))
        {
            throw new ArgumentNullException();
        }

        // Remove the leading dot if present
        extension = extension.TrimStart('.').ToLower();

        return extension switch
        {
            // Text files
            "txt" => FileType.Txt,
            "csv" => FileType.Csv,
            "json" => FileType.Json,
            "xml" => FileType.Xml,
            "html" => FileType.Html,

            // Document files
            "doc" => FileType.Doc,
            "docx" => FileType.Docx,
            "pdf" => FileType.Pdf,
            "xls" => FileType.Xls,
            "xlsx" => FileType.Xlsx,
            "ppt" => FileType.Ppt,
            "pptx" => FileType.Pptx,

            // Image files
            "jpg" => FileType.Jpg,
            "jpeg" => FileType.Jpeg,
            "png" => FileType.Png,
            "gif" => FileType.Gif,
            "bmp" => FileType.Bmp,
            "svg" => FileType.Svg,

            // Audio files
            "mp3" => FileType.Mp3,
            "wav" => FileType.Wav,
            "flac" => FileType.Flac,

            // Video files
            "mp4" => FileType.Mp4,
            "avi" => FileType.Avi,
            "mkv" => FileType.Mkv,
            "mov" => FileType.Mov,

            // Compressed files
            "zip" => FileType.Zip,
            "rar" => FileType.Rar,
            "tar" => FileType.Tar,
            "gz" => FileType.Gz,

            // Executable files
            "exe" => FileType.Exe,
            "dll" => FileType.Dll,
            "bat" => FileType.Bat,
            "sh" => FileType.Sh,

            // Other common file types
            "iso" => FileType.Iso,
            "sql" => FileType.Sql,
            "css" => FileType.Css,
            "js" => FileType.Js,
            "php" => FileType.Php,

            _ => throw new NotSupportedTypeException()
        };
    }
}
