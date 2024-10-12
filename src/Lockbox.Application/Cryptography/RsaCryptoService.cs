using System.Security.Cryptography;
using System.Text;

namespace Lockbox.Application.Cryptography;

public static class RsaCryptoService
{
    public static (string, string) GenerateKey()
    {
        using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        string publicKey = rsa.ToXmlString(false);
        string privateKey = rsa.ToXmlString(true);
        return (publicKey, privateKey);
    }

    public static byte[] Encrypt(string publicKey, string dataToEncrypt)
    {
        using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(publicKey);
        byte[] messageBytes = Encoding.UTF8.GetBytes(dataToEncrypt);
        byte[] encryptedBytes = rsa.Encrypt(messageBytes, RSAEncryptionPadding.Pkcs1);
        return encryptedBytes;
    }

    public static string Decrypt(string privateKey, byte[] encryptedBytes)
    {
        using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey);
        byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
        string decryptedMessage = Encoding.UTF8.GetString(decryptedBytes);
        return decryptedMessage;
    }
}