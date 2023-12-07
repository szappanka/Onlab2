using Common.Models;
using Common.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient httpClient;

        public TaskService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        //public async Task<FileContentResult> GetFileById(int fileId)
        //{
        //    var file = await httpClient.GetAsync("api/files/" + fileId);
        //    return file as FileContentResult;
        //}

        public async Task<IEnumerable<EscapeRoomTaskDto>> GetTasks()
        {
            try
            {
                var tasks = await httpClient.GetFromJsonAsync<IEnumerable<EscapeRoomTaskDto>>("api/Tasks");
                return tasks;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<EscapeRoomTaskDto>> GetTasksByCourseId(int courseId)
        {
            var tasks = await httpClient.GetFromJsonAsync<IEnumerable<EscapeRoomTaskDto>>($"api/Tasks/course/{courseId}");
            return tasks;
        }

        public async Task SendHint(int taskId, string hint)
        {
            await httpClient.PutAsync($"api/Tasks/{taskId}/hint?hint={hint}", new StringContent(""));
            return;
        }
    }
}
