using Authentification.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Models
{
    public class SLog : BaseEntity
    {
        public DateTime time { get; set; }
        public long taskid { get; set; }
        public Tasks task_id { get; set; }
    }
}
