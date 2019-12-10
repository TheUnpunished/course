using Authentification.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Models
{
    public class People : BaseEntity
    {
        public string user { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string language { get; set; }
        public bool admin { get; set; }
        public string password { get; set; }
    }
}
