using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor_Mongo_Dev.Client;
using RestSharp;
using Blazor_Mongo_Dev.Client.Services;
using Blazored.Modal;
using Blazored.Toast;
using CurrieTechnologies.Razor.SweetAlert2;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<BlogService>();
builder.Services.AddSweetAlert2();

builder.Services.AddSweetAlert2(options => {
    options.Theme = SweetAlertTheme.Default;
});
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredModal();


builder.Services.AddSingleton(sp => {
    var restClient = new RestClient(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    restClient.AddDefaultHeader("X-Requested-With", "XMLHttpRequest");

    return restClient;
}
);


await builder.Build().RunAsync();

