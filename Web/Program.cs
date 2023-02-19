using Core.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Web.Extensions;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddInfrastructure(config.GetConnectionString("DatabaseConnection"));
services.AddServices();
services.AddAuth(config);
services.AddUtilities(config);

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<GlobalErrorsHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();