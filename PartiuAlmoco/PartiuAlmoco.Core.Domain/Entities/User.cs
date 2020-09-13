using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartiuAlmoco.Core.Domain.Entities
{
    public class User : Entity
    {
        public string FullName { get; private set; }
        public string FriendlyName { get; private set; }
        public string Email { get; private set; }

        public User(Guid id, string fullName, string friendlyName, string email)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            Guard.Against.NullOrWhiteSpace(fullName, nameof(fullName));
            Guard.Against.NullOrWhiteSpace(friendlyName, nameof(friendlyName));
            Guard.Against.InvalidEmail(email, nameof(email));

            Id = id;
            FullName = fullName;
            FriendlyName = friendlyName;
            Email = email;
        }

        // Entity Framework
        protected User() { }
    }
}
