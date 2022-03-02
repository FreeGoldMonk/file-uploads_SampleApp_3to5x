/*
install-package Microsoft.EntityFrameworkCore -Version 5.0.12; install-package Microsoft.EntityFrameworkCore.SqlServer -Version 5.0.12; install-package Microsoft.EntityFrameworkCore.Tools -Version 5.0.12;
add-migration Init
update-database
*/
using Microsoft.EntityFrameworkCore;
using file_uploads_SampleApp_3to5x.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace file_uploads_SampleApp_3to5x.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppFile> File { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                              .AddJsonFile("appsettings.json")
                              .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                              .EnableSensitiveDataLogging();

            }
        }
    }
}
