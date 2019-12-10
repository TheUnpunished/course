using Authentification.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Models
{
    public class User : BaseEntity
    {
        public string email { get; set; }
        public string password { get; set; }

        public static explicit operator User(Task<User> v)
        {
            throw new NotImplementedException();
        }
    }
}
