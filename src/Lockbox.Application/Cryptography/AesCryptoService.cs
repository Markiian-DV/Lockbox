using System.Security.Cryptography;

namespace Lockbox.Application.Cryptography;

public static class AesCryptoService
{
    public static async Task EncryptFileAsync(Guid key, Stream inputStream, Stream outputStream)
    {
        using Aes aes = Aes.Create();
        aes.Key = key.ToByteArray();
        aes.IV = new byte[16];
        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using CryptoStream cryptoStream = new(outputStream, encryptor, CryptoStreamMode.Write);
        await inputStream.CopyToAsync(cryptoStream);
        await cryptoStream.FlushFinalBlockAsync();
    }

    public static async Task DecryptFileAsync(Guid key, Stream inputStream, Stream outputStream)
    {
        using Aes aes = Aes.Create();
        aes.Key = key.ToByteArray();
        aes.IV = new byte[16];
        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using CryptoStream cryptoStream = new(outputStream, decryptor, CryptoStreamMode.Write);
        await inputStream.CopyToAsync(cryptoStream);
        await cryptoStream.FlushFinalBlockAsync();
    }

    public static Guid CreateFileAccessKey() => Guid.NewGuid();
}