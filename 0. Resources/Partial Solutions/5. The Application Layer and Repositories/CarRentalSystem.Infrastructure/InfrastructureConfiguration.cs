namespace CarRentalSystem.Infrastructure
{
    using Application.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using Persistence.Repositories;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<CarRentalDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(CarRentalDbContext)
                            .Assembly.FullName)))
                .AddTransient(typeof(IRepository<>), typeof(DataRepository<>));
    }
}
