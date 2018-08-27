namespace Msi.UtilityKit.Cryptography
{
    public interface IDigitalSignatureService
    {
        Signature SignHash(Hash hash, CryptoKey privateKey);
        bool VerifyHash(Hash hash, Signature signature, CryptoKey publicKey);
    }
}