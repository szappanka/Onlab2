using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class EscapeRoomCourse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<EscapeRoomTask> EscapeRoomTasks { get; set; } = new List<EscapeRoomTask>();
    }
}
