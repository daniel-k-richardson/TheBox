using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheBox.Domain.Users.Interfaces;
using TheBox.Persistence.Users.DatabaseContext;
using TheBox.Persistence.Users.Repositories;

namespace TheBox.Persistence;

public static class PersistenceInjection
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("UserConnection"),
                b => b.MigrationsAssembly(typeof(UserDbContext).Assembly.FullName));
        });

        services.AddScoped<IUserRepository, UserRepository>();
    }
}