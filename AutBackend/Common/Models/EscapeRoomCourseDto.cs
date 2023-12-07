namespace Common.Models
{
    public class EscapeRoomCourseDto
    {
        public int Id { get; set; }
        public ICollection<EscapeRoomTaskDto> EscapeRoomTasks { get; set; } = new List<EscapeRoomTaskDto>();
    }
}
