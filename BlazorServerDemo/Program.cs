using BlazorServerDemo.Features;
using DataLibrary.Data;
using DataLibrary.DataAccess;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddHubOptions(options =>
    {
        options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
        options.HandshakeTimeout = TimeSpan.FromSeconds(30);
    });
builder.Services.AddResponseCompression();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IFoodRepository, FoodRepository>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
var app = builder.Build();

if (app.Environment.IsDevelopment() == false)
{
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AllowAnonymous();
app.Run();