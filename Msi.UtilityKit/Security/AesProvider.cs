using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Msi.UtilityKit.Security
{
    // Advanced Encryption Standard Provider
    public class AesProvider : IAesProvider
    {

        private readonly ICryptoTransform _encryptor, _decryptor;
        private UTF8Encoding _encoder;

        public AesProvider(string key, string secret)
        {

            _encoder = new UTF8Encoding();

            var _key = _encoder.GetBytes(key);
            var _secret = _encoder.GetBytes(secret);

            var managedAlgorithm = Aes.Create();
            managedAlgorithm.BlockSize = 128;
            managedAlgorithm.KeySize = 128;

            _encryptor = managedAlgorithm.CreateEncryptor(_key, _secret);
            _decryptor = managedAlgorithm.CreateDecryptor(_key, _secret);
        }

        /// <summary>
        /// Encrypt string
        /// </summary>
        public string Encrypt(string unencrypted)
        {
            return Convert.ToBase64String(Encrypt(this._encoder.GetBytes(unencrypted)));
        }

        /// <summary>
        /// Decrypt string
        /// </summary>
        public string Decrypt(string encrypted)
        {
            return _encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)));
        }

        /// <summary>
        /// Encrypt bytes
        /// </summary>
        public byte[] Encrypt(byte[] buffer)
        {
            return Transform(buffer, _encryptor);
        }

        /// <summary>
        /// Decrypt bytes
        /// </summary>
        public byte[] Decrypt(byte[] buffer)
        {
            return Transform(buffer, _decryptor);
        }

        /// <summary>
        /// Writes bytes to memory
        /// </summary>
        protected byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {            var stream = new MemoryStream();
            using (var cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }
            return stream.ToArray();
        }
    }
}
