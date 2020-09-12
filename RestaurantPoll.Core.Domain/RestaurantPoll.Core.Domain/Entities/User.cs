using System;
using System.Collections.Generic;
using System.Text;

namespace PartiuAlmoco.Core.Domain.Entities
{
    public class User : Entity
    {
        public string FullName { get; set; }
        public string FriendlyName { get; set; }
        public string Email { get; set; }
    }
}
