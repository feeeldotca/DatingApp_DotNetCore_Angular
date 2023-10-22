
using API.Data;
using API.Interface;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.ServiceExtensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServicesExtension(this IServiceCollection services, IConfiguration config){
            services.AddDbContext<DataContext>(opt=> opt.UseSqlite(
            config.GetConnectionString("DefaultConnection")) );
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
        
    }
}