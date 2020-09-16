using PartiuAlmoco.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Core.Domain.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetById(Guid id);
        User CreateUser(string fullName, string friendlyName, string email, string passwordHash);
        string RetrieveUserPasswordHashBase64(Guid userId);
        User GetUserByEmail(string email);
    }
}
