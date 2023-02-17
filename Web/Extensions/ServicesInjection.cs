using Infrastructure.Context;
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
        }
    }
}
