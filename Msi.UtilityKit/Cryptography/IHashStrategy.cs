namespace Msi.UtilityKit.Cryptography
{
    public interface IHashStrategy
    {
        Hash ComputeHash(byte[] data);
    }
}