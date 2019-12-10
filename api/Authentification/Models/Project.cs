using Authentification.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Models
{
    public class Project : BaseEntity
    {
        public string name {get; set;}
        public string description { get; set; }
        public bool isPublic { get; set; }
        public long userid { get; set; }
        public string repository { get; set; }
    }
}
