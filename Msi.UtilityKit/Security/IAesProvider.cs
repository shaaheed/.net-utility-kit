namespace Msi.UtilityKit.Security
{
    public interface IAesProvider
    {
        string Encrypt(string unencrypted);
        string Decrypt(string encrypted);
        byte[] Encrypt(byte[] buffer);
        byte[] Decrypt(byte[] buffer);
    }
}
