using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities

{
    public class EscapeRoomTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ImageId { get; set; }
        public int PrefabId { get; set; }
        public string? Hint { get; set; }
        public bool Completed { get; set; }
        public ICollection<EscapeRoomCourse> EscapeRoomCourses { get; set; } = new List<EscapeRoomCourse>();
    }
}
