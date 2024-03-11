using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CloudLibrary.Client;
using Microsoft.AspNetCore.Components.Authorization;
using CloudLibrary.Client.Providers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddLibraryClient()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}data-api/graphql"));

builder.Services.AddHttpClient(
    "AuthClient",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services
    .AddAuthorizationCore()
    .AddScoped<AuthenticationStateProvider, CloudLibraryAuthenticationStateProvider>();

await builder.Build().RunAsync();
