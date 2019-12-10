using Authentification.Repositories.Entities;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Models
{
    public class Tasks : BaseEntity
    {
        public string theme { get; set; }
        public long trackerid { get; set; }
        public Tracker tracker { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public string description { get; set; }
        public long categoryid { get; set; }
        public Category category { get; set; }
        public long springid { get; set; }
        public Spring spring { get; set; }
        public DateTime date_over { get; set; }
        public int time { get; set; }
        public string percent { get; set; }
        public string file_path { get; set; }
        public long whocanid { get; set; }
        public People whoCan { get; set; }
        public Project project_id { get; set; }
        public long projectid { get; set; }
    }
}
