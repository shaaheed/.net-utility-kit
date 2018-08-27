namespace Msi.UtilityKit.Cryptography
{
    public abstract class CryptoKeyPair
    {
        private readonly CryptoKey privateKey;
        private readonly CryptoKey publicKey;

        protected CryptoKeyPair(CryptoKey privateKey, CryptoKey publicKey)
        {
            this.privateKey = privateKey;
            this.publicKey = publicKey;
        }

        public CryptoKey PrivateKey => privateKey;

        public CryptoKey PublicKey => publicKey;
    }
}