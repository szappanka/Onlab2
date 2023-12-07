using Common.Models;
using Common.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace GameMaster.Pages
{
    public class TasksBase : ComponentBase
    {
        [Inject]
        public ITaskService TaskService { get; set; }
        [Parameter]
        public int CourseId { get; set; }
        public IEnumerable<EscapeRoomTaskDto> Tasks { get; set; }
        public Dictionary<int, string> ImageIdToPath { get; set; }
        private Timer _timer;
        public string hintFieldText;

        protected override async Task OnInitializedAsync()
        {
            Tasks = await TaskService.GetTasksByCourseId(CourseId);
            foreach (var t in Tasks)
            {
                //get image paths

            }
            _timer = new Timer(DoPeriodicCall, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        }


        private async void DoPeriodicCall(Object state)
        {
            var oldTasks = Tasks.ToList();
            Tasks = await TaskService.GetTasksByCourseId(CourseId);
            int i = 0;
            bool stateHasChanged = false;
            foreach(var t in Tasks)
            {
                if(t.Completed != oldTasks[i].Completed || t.Hint != oldTasks[i].Hint)
                {
                    stateHasChanged = true;
                }
                i++;
            }
            if(stateHasChanged) await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        
    }
}
