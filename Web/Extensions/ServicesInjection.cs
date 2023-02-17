using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Services;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Web.Extensions
{
    internal static class ServicesInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, string dbConnString)
        {
            services.AddDbContext<MeetupDbContext>(opt =>
            {
                opt.UseSqlServer(dbConnString);
            });

            services.AddScoped<IMeetupsRepository, MeetupsRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMeetupsService, MeetupsService>();
        }
    }
}
