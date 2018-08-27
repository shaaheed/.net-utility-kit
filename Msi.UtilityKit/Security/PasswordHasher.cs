using System;

namespace Msi.UtilityKit.Security
{
    public class PasswordHasher : IPasswordHasher
    {

        private readonly IAesProvider _provider;

        public PasswordHasher(IAesProvider provider)
        {
            _provider = provider;
        }

        public string HashPassword(string password)
        {
            return _provider.Encrypt(password);
        }

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
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
    }
}
