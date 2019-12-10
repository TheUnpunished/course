using Authentification.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Models
{
    public class Log : BaseEntity
    {
        public string name { get; set; }
        public long taskid { get; set; }
        public Tasks task_id { get; set; }
        public string description { get; set; }
        public string file_path { get; set; }
    }
}
