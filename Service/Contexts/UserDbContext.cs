using Microsoft.EntityFrameworkCore;
using Service.EnumHelpers;
using Service.Models;

namespace Service.Contexts
{
    public class UserDbContext : DbContext
    {


        protected readonly IConfiguration Configuration;
        public UserDbContext(DbContextOptions<UserDbContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            var connectionString = Configuration.GetConnectionString("mysql");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Status> Statuses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Ignore(s => s.Status)
                .Property<int>("_statusId")
                .HasColumnName("StatusId")
                .IsRequired();


        }
    }


}
