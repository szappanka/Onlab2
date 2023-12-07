namespace Common.Models
{
    public class EscapeRoomTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ImageId { get; set; }
        public int PrefabId { get; set; }
        public string? Hint { get; set; }
        public bool Completed { get; set; }
        public int CourseId { get; set; }
        public ICollection<EscapeRoomCourseDto> EscapeRoomCourses { get; set; } = new List<EscapeRoomCourseDto>();

    }
}