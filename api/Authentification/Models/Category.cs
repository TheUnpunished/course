using Authentification.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Models
{
    public class Category : BaseEntity
    {
        public string name { get; set; }
        public long peopleid { get; set; }
        public People people_id { get; set; }
    }
}
