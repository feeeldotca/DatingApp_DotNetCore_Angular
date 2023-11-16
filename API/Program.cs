using System.Data.Common;
using API.Data;
using API.Services;
using API.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.ServiceExtensions;
using API.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt=> opt.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")) );
builder.Services.AddCors();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddApplicationServicesExtension(builder.Configuration);
builder.Services.AddIdentityServiceExtension(builder.Configuration);

var app = builder.Build();
// use middleware created in fold MiddleWare
app.UseMiddleware<ExceptionMiddleWare>();
app.UseCors(builder=>builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins("https://localhost:4200"));

app.UseHttpsRedirection();

//Authenttication is for if you are qualified to do  
app.UseAuthentication();
//authorization is for what you can do
app.UseAuthorization();

app.MapControllers();

app.Run();
