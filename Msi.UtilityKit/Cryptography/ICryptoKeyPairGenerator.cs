namespace Msi.UtilityKit.Cryptography
{
    public interface ICryptoKeyPairGenerator<out TCryptoKeyPair> where TCryptoKeyPair : CryptoKeyPair
    {
        TCryptoKeyPair GenerateKeyPair();
    }
}