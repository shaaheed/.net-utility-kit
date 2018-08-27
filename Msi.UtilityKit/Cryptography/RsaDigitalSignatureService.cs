namespace Msi.UtilityKit.Cryptography
{
    using System.Security.Cryptography;

    public sealed class RsaDigitalSignatureService
    {

        private readonly int keySize;
        private readonly HashAlgorithmName name;
        private readonly RSASignaturePadding padding;

        public RsaDigitalSignatureService(int keySize, HashAlgorithmName name, RSASignaturePadding padding)
        {
            this.keySize = keySize;
            this.name = name;
            this.padding = padding;
        }

        public Signature SignHash(Hash hash, CryptoKey privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportCryptoKey(privateKey);

                return Signature.FromBytes(rsa.SignHash(hash.Data, name, padding));
            }
        }

        public bool VerifyHash(Hash hash, Signature signature, CryptoKey publicKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportCryptoKey(publicKey);

                return rsa.VerifyHash(hash.Data, signature.Data, name, padding);
            }
        }
    }
}