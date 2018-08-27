using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Msi.UtilityKit.Cryptography
{
    public static class CryptographyUtilities
    {

        public static CryptoKey GetPrivateKey(this RSACryptoServiceProvider provider)
        {
            RSAParameters parameters = provider.ExportParameters(true);
            string json = JsonConvert.SerializeObject(parameters);
            byte[] data = Encoding.UTF8.GetBytes(json);
            return CryptoKey.FromBytes(data);
        }

        public static CryptoKey GetPublicKey(this RSACryptoServiceProvider provider)
        {
            RSAParameters parameters = provider.ExportParameters(false);
            string json = JsonConvert.SerializeObject(parameters);
            byte[] data = Encoding.UTF8.GetBytes(json);
            return CryptoKey.FromBytes(data);
        }

        public static void ImportCryptoKey(this RSACryptoServiceProvider provider, CryptoKey key)
        {
            string json = Encoding.UTF8.GetString(key.Data);
            RSAParameters parameters = JsonConvert.DeserializeObject<RSAParameters>(json);
            provider.ImportParameters(parameters);
        }

        public static RsaCryptoKeyPair GetCryptoKeyPair(this RSACryptoServiceProvider provider)
        {
            return RsaCryptoKeyPair.FromKeys(provider.GetPrivateKey(), provider.GetPublicKey(), provider.KeySize);
        }

    }
}
