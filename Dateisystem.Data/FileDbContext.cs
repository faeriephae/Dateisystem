using Dateisystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dateisystem.Data
{
    public class FileDbContext : DbContext
    {
        private static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FileDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public FileDbContext(){}
        public static FileDbContext GetContext() 
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(connectionString);
            return new FileDbContext(builder.Options);
        }

        public FileDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Reflection
            var entities = GetType().Assembly.GetExportedTypes()
                .Where(x => typeof(IEntity).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract && !x.IsInterface);
            foreach(var entity in entities)
            {
                modelBuilder.Entity(entity);
            }
        }
    }
}