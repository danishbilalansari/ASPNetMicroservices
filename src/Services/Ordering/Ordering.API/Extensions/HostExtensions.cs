using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Extensions
{
    public static class HostExtensions
    {
        /// <summary>
        /// Applies any pending migrations for the specified <typeparamref name="TContext"/> database context 
        /// and executes a seeder action. Implements a basic retry mechanism in case of SQL exceptions.
        /// </summary>
        /// <typeparam name="TContext">The type of the <see cref="DbContext"/> to migrate.</typeparam>
        /// <param name="host">The application host.</param>
        /// <param name="seeder">A delegate that seeds the database using the context and service provider.</param>
        /// <param name="retry">The current retry attempt count (default is 0).</param>
        /// <returns>The application host after performing the migration and seeding.</returns>
        /// <exception cref="SqlException">May rethrow if retry limit is reached during a SQL failure.</exception>
        public static IHost MigrateDatabase<TContext>(this IHost host, 
                                                        Action<TContext, 
                                                        IServiceProvider> seeder, 
                                                        int? retry = 0) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    InvokeSeeder(seeder, context, services);

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retryForAvailability);
                    }
                }
            }
            return host;
        }

        /// <summary>
        /// Invokes the database migration and executes the provided seeder logic.
        /// </summary>
        /// <typeparam name="TContext">The type of the <see cref="DbContext"/> being seeded.</typeparam>
        /// <param name="seeder">The seeder action to execute.</param>
        /// <param name="context">The database context instance.</param>
        /// <param name="services">The application's service provider.</param>
        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder,
                                                   TContext context,
                                                   IServiceProvider services)
                                                   where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
