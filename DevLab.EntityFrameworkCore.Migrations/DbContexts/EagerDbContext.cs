using DevLab.EntityFrameworkCore.Migrations.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevLab.EntityFrameworkCore.Migrations.DbContexts
{
    public class EagerDbContext : DbContext, IDesignTimeDbContextFactory<EagerDbContext>
    {
        public EagerDbContext() : this(new DbContextOptions<EagerDbContext>())
        {
        }

        public EagerDbContext(DbContextOptions<EagerDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .HasKey(x => x.Id);
        }

        public EagerDbContext CreateDbContext(string[] args)
        {
            DbContextOptions<EagerDbContext> options = new DbContextOptionsBuilder<EagerDbContext>()
                .UseSqlite("Data Source=LocalDatabase.db")
                .ReplaceService<IMigrationsAnnotationProvider, Sqlite.SqliteMigrationsAnnotationProvider>()
                .ReplaceService<IMigrationsSqlGenerator, Sqlite.SqliteMigrationsSqlGenerator>()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .Options;

            return new EagerDbContext(options);
        }
    }
}