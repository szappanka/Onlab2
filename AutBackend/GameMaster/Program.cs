using Common.Services;
using Common.Services.Contracts;
using GameMaster;
using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7200/") });
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
