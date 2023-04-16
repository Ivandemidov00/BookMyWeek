using BookMyWeek.Application;
using BookMyWeek.Application.Authentication;
using BookMyWeek.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddCookieAuthentication();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseRouting();
app.UseSwagger().UseSwaggerUI();
app.UseCookieAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers().RequireAuthorization());

await app.RunAsync();