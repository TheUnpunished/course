using Authentification.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Models
{
    public class Tracker : BaseEntity
    {
        public string name { get; set; }
        public string status { get; set; }
    }
}
