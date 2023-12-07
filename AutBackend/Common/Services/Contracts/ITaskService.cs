using Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Contracts
{
    public interface ITaskService
    {
        Task<IEnumerable<EscapeRoomTaskDto>> GetTasks();
        Task<IEnumerable<EscapeRoomTaskDto>> GetTasksByCourseId(int courseId);
        //FileContentResult GetFileById(int fileId);
        Task SendHint(int taskId, string hint);
    }
}
