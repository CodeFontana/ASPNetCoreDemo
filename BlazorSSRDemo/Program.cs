using BlazorSSRDemo.Features;
using DataLibrary.Data;
using DataLibrary.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddRazorComponents();
builder.Services.AddSingleton<IDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IFoodRepository, FoodRepository>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapDefaultEndpoints();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AllowAnonymous();
app.Run();
