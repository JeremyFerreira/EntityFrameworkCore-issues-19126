using System;
using System.Security.Authentication.ExtendedProtection;
using System.Threading;
using System.Threading.Tasks;
using DevLab.EntityFrameworkCore.Migrations.DbContexts;
using DevLab.EntityFrameworkCore.Migrations.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DevLab.EntityFrameworkCore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EagerDbContext>(
                configure =>
                {
                    configure.UseSqlite("Data Source=LocalDatabase.db");
                    //.ReplaceService<IMigrationsAnnotationProvider, Sqlite.SqliteMigrationsAnnotationProvider>()
                    //.ReplaceService<IMigrationsSqlGenerator, Sqlite.SqliteMigrationsSqlGenerator>()
                    configure.EnableDetailedErrors();
                    configure.EnableSensitiveDataLogging();
                });

            ServiceProvider provider = serviceCollection.BuildServiceProvider();

            using (IServiceScope scope = provider.CreateScope())
            {
                await StartAsync(scope.ServiceProvider);
            }

            Console.ReadLine();
        }

        public static async Task StartAsync(IServiceProvider provider)
        {
            var dbContext = provider.GetRequiredService<EagerDbContext>();

            await dbContext.Person.AddAsync(
                new Person()
                {
                    Name = "Test"
                });
        }
    }
}