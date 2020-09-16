using Ardalis.GuardClauses;
using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Infra.Domain
{
    public class UserRepository : IUserRepository
    {
        public PartiuAlmocoDbContext dbContext = null;

        public UserRepository(PartiuAlmocoDbContext dbContext)
        {
            Guard.Against.Null(dbContext, nameof(dbContext));

            this.dbContext = dbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return dbContext.Users.ToList();
        }

        User IUserRepository.GetById(Guid id)
        {
            return dbContext.Users.Find(id);
        }

        public User CreateUser(string fullName, string friendlyName, string email, string passwordHash)
        {
            Guard.Against.NullOrWhiteSpace(fullName, nameof(fullName));
            Guard.Against.NullOrWhiteSpace(friendlyName, nameof(friendlyName));
            Guard.Against.NullOrWhiteSpace(email, nameof(email));
            Guard.Against.NullOrWhiteSpace(passwordHash, nameof(passwordHash));

            var newUser = new User(Guid.NewGuid(), fullName, friendlyName, email);
            var newUserPassword = new UserPassword(Guid.NewGuid(), newUser, passwordHash);

            dbContext.Add(newUser);
            dbContext.Add(newUserPassword);
            dbContext.SaveChanges();

            return newUser;
        }

        public string RetrieveUserPasswordHashBase64(Guid userId)
        {
            Guard.Against.NullOrEmpty(userId, nameof(userId));
            return dbContext.UserPasswords.First(up => up.User.Id == userId).PasswordHashBase64;
        }

        public User GetUserByEmail(string email)
        {
            Guard.Against.InvalidEmail(email, nameof(email));
            return dbContext.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
