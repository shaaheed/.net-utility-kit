using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Msi.UtilityKit.Security
{
    public static class SecurityUtilities
    {

        private static AesOptions _aesOptions = new AesOptions();
        private static ICryptoTransform _encryptor;
        private static ICryptoTransform _decryptor;
        private static UTF8Encoding _encoder;

        static SecurityUtilities()
        {

        }

        public static void ConfigureOptions(Action<AesOptions> aesOptions)
        {
            aesOptions?.Invoke(_aesOptions);

            if (_aesOptions == null)
            {
                throw new NullReferenceException("AES options is null. Please set the AES options");
            }

            if (string.IsNullOrEmpty(_aesOptions.Key))
            {
                throw new ArgumentNullException("AES key is null or empty");
            }

            if (string.IsNullOrEmpty(_aesOptions.Secret))
            {
                throw new ArgumentNullException("AES secret is null or empty");
            }

            if (_encoder == null)
            {
                _encoder = new UTF8Encoding();
            }

            var _key = _encoder.GetBytes(_aesOptions.Key);
            var _secret = _encoder.GetBytes(_aesOptions.Secret);

            if (_encryptor == null || _decryptor == null)
            {
                var managedAlgorithm = Aes.Create();
                managedAlgorithm.BlockSize = 128;
                managedAlgorithm.KeySize = 128;

                _encryptor = managedAlgorithm.CreateEncryptor(_key, _secret);
                _decryptor = managedAlgorithm.CreateDecryptor(_key, _secret);
            }

        }

        public static string Encrypt(this string unencrypted)
        {
            return Convert.ToBase64String(Encrypt(_encoder.GetBytes(unencrypted)));
        }

        public static string Decrypt(this string encrypted)
        {
            return _encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)));
        }

        public static byte[] Encrypt(this byte[] buffer)
        {
            return Transform(buffer, _encryptor);
        }

        public static byte[] Decrypt(byte[] buffer)
        {
            return Transform(buffer, _decryptor);
        }

        public static string HashPassword(this string password)
        {
            return Encrypt(password);
        }

        public static bool VerifyHashedPassword(this string hashedPassword, string providedPassword)
        {

            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new ArgumentNullException("Hased password is null or empty");
            }

            if (string.IsNullOrEmpty(providedPassword))
            {
                throw new ArgumentNullException("Provided password is null or empty");
            }

            if (hashedPassword.Equals(HashPassword(providedPassword)))
                return true;

            return false;
        }

        static byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            var stream = new MemoryStream();
            using (var cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }
            return stream.ToArray();
        }

    }
}
