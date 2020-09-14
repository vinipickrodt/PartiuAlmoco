using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartiuAlmoco.Core.Domain.Entities
{
    public class UserPassword : Entity
    {
        public User User { get; set; }
        public string PasswordHashBase64 { get; private set; }

        public UserPassword(Guid id, User user, string passwordBase64)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(passwordBase64, nameof(passwordBase64));

            Id = id;
            User = user;
            PasswordHashBase64 = passwordBase64;
        }

        // Entity Framework
        protected UserPassword() { }
    }
}
