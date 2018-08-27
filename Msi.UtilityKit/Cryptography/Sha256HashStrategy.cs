namespace Msi.UtilityKit.Cryptography
{
    using System.Security.Cryptography;

    public sealed class Sha256HashStrategy : IHashStrategy
    {
        public Hash ComputeHash(byte[] data)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                return Hash.FromBytes(algorithm.ComputeHash(data));
            }
        }
    }
}