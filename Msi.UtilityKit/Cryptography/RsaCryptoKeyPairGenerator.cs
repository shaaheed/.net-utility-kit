namespace Msi.UtilityKit.Cryptography
{
    using System.Security.Cryptography;

    public class RsaCryptoKeyPairGenerator : ICryptoKeyPairGenerator<RsaCryptoKeyPair>
    {
        private readonly int keySize;

        public RsaCryptoKeyPairGenerator(int keySize)
        {
            this.keySize = keySize;
        }

        public RsaCryptoKeyPair GenerateKeyPair()
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize))
            {
                rsa.PersistKeyInCsp = false;
                return rsa.GetCryptoKeyPair();
            }
        }
    }
}