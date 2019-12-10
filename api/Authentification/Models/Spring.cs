using Authentification.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Models
{
    public class Spring : BaseEntity
    {
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public long wikiid { get; set; }
        public Wiki wiki { get; set; }
        public DateTime date { get; set; }
    }
}
