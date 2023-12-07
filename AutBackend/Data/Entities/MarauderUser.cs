using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class MarauderUser
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int LastUpdate { get; set; }
        public bool IsActivated { get; set; }
        public string? Coordinates { get; set; }

    }
}
