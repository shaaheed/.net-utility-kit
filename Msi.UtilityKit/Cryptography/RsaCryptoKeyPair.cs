namespace Msi.UtilityKit.Cryptography
{
    public sealed class RsaCryptoKeyPair : CryptoKeyPair
    {
        public static readonly int DefaultKeySize = 2048;

        private readonly int keySize;

        private RsaCryptoKeyPair(CryptoKey privateKey, CryptoKey publicKey, int keySize) : base(privateKey, publicKey)
        {
            this.keySize = keySize;
        }

        public int KeySize => keySize;

        public static RsaCryptoKeyPair FromKeys(CryptoKey privateKey, CryptoKey publicKey, int keySize)
        {
            return new RsaCryptoKeyPair(privateKey, publicKey, keySize);
        }
    }
}