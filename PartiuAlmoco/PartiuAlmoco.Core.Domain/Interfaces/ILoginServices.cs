using PartiuAlmoco.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Core.Domain.Interfaces
{
    public interface ILoginServices
    {
        User Authenticate(string email, string password);
        string ConvertPasswordToHash(string password);
        User CreateUser(string fullName, string friendlyName, string email, string password);
    }
}
