using Common.Models;

namespace Common.Services.Contracts
{
  public interface ICourseService
  {
    Task<IEnumerable<EscapeRoomCourseDto>> GetCourses();
  }
}
