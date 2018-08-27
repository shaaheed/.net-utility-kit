namespace Msi.UtilityKit.Cryptography
{
    using System;
    using System.Linq;

    public sealed class CryptoKey : IEquatable<CryptoKey>
    {
        private readonly byte[] data;

        private CryptoKey(byte[] data)
        {
            this.data = data;
        }

        public byte[] Data => data;

        public static bool operator ==(CryptoKey a, CryptoKey b)
        {
            return Equals(a, b);
        }

        public static bool operator !=(CryptoKey a, CryptoKey b)
        {
            return !Equals(a, b);
        }

        public static CryptoKey FromBytes(byte[] value)
        {
            return new CryptoKey(value);
        }

        public static CryptoKey FromString(string value)
        {
            return new CryptoKey(Convert.FromBase64String(value));
        }

        public bool Equals(CryptoKey other)
        {
            return !ReferenceEquals(other, null)
                && data.SequenceEqual(other.data);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(obj, null)
                && obj is CryptoKey
                && Equals(obj as CryptoKey);
        }

        public override int GetHashCode()
        {
            return data.GetHashCode();
        }

        public override string ToString()
        {
            return Convert.ToBase64String(data);
        }
    }
}