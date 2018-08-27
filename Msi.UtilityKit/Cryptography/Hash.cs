namespace Msi.UtilityKit.Cryptography
{
    using System;
    using System.Linq;

    public sealed class Hash : IEquatable<Hash>
    {
        private readonly byte[] data;

        private Hash(byte[] data)
        {
            this.data = data;
        }

        public byte[] Data => data;

        public static bool operator ==(Hash a, Hash b)
        {
            return Equals(a, b);
        }

        public static bool operator !=(Hash a, Hash b)
        {
            return !Equals(a, b);
        }

        public static Hash FromBytes(byte[] data)
        {
            return new Hash(data);
        }

        public static Hash FromGuid(Guid id)
        {
            return new Hash(id.ToByteArray());
        }

        public static Hash FromString(string value)
        {
            return new Hash(Enumerable.Range(0, value.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(value.Substring(x, 2), 16))
                     .ToArray());
        }

        public bool Equals(Hash other)
        {
            return !ReferenceEquals(other, null)
                && data.SequenceEqual(other.data);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(obj, null)
                && obj is Hash
                && Equals(obj as Hash);
        }

        public override int GetHashCode()
        {
            return data.GetHashCode();
        }

        public override string ToString()
        {
            return BitConverter
                .ToString(data)
                .Replace("-", string.Empty);
        }

        public Guid ToGuid()
        {
            if (data.Length != 16)
            {
                throw new InvalidCastException("Hash must be 16 bytes long to convert to System.Guid");
            }

            return new Guid(data);
        }
    }
}