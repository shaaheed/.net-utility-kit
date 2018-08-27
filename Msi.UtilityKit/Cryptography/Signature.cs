namespace Msi.UtilityKit.Cryptography
{
    using System;
    using System.Linq;

    public sealed class Signature : IEquatable<Signature>
    {
        private readonly byte[] data;

        private Signature(byte[] data)
        {
            this.data = data;
        }

        public byte[] Data => data;

        public static bool operator ==(Signature a, Signature b)
        {
            return Equals(a, b);
        }

        public static bool operator !=(Signature a, Signature b)
        {
            return !Equals(a, b);
        }

        public static Signature FromBytes(byte[] value)
        {
            return new Signature(value);
        }

        public static Signature FromString(string value)
        {
            return new Signature(Convert.FromBase64String(value));
        }

        public bool Equals(Signature other)
        {
            return !ReferenceEquals(other, null)
                && data.SequenceEqual(other.data);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(obj, null)
                && obj is Signature
                && Equals(obj as Signature);
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