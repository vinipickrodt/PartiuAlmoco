using Ardalis.GuardClauses;
using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Interfaces;
using PartiuAlmoco.Core.Domain.Utils;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PartiuAlmoco.Application
{
    public class LoginServices : ILoginServices
    {
        private const string SALT = "Gmy4I3N2fQgS6lQ6uw9JVW6jpBuiTlI50WVN0oHix63WsAAgeJ";

        private readonly IUserRepository userRepository;

        public User Authenticate(string email, string password)
        {
            Guard.Against.InvalidEmail(email, nameof(email));
            Guard.Against.NullOrWhiteSpace(password, nameof(password));

            User user = userRepository.GetUserByEmail(email);

            if (user == null)
            {
                return null;
            }

            var passwordHashBase64 = userRepository.RetrieveUserPasswordHashBase64(user.Id);
            var hashesMatch = ConvertPasswordToHash(password) == passwordHashBase64;

            if (hashesMatch)
            {
                return user;
            }

            return null;
        }

        public string ConvertPasswordToHash(string password)
        {
            Guard.Against.NullOrWhiteSpace(password, nameof(password));

            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashBytes = sha256hasher.ComputeHash(Encoding.Unicode.GetBytes(SALT + password));
            return Convert.ToBase64String(hashBytes);
        }

        public User CreateUser(string fullName, string friendlyName, string email, string password)
        {
            Guard.Against.NullOrWhiteSpace(fullName, nameof(fullName));
            Guard.Against.NullOrWhiteSpace(friendlyName, nameof(friendlyName));
            Guard.Against.InvalidEmail(email, nameof(email));
            Guard.Against.NullOrWhiteSpace(password, nameof(password));

            return userRepository.CreateUser(fullName, friendlyName, email, ConvertPasswordToHash(password));
        }

        public LoginServices(IUserRepository userRepository)
        {
            Guard.Against.Null(userRepository, nameof(userRepository));
            this.userRepository = userRepository;
        }
    }
}
