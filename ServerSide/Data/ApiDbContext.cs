using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using _TicketSystem.Models;
using Microsoft.AspNetCore.Identity;


namespace _TicketSystem.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<Billing> Billings { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<Asset> Assets { get; set; }

        public virtual DbSet<Agreement> Agreements{ get; set; }

        public virtual DbSet<Status> Statuses{ get; set; }
        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "superadmin",
                NormalizedName = "superadmin"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "admin",
                NormalizedName = "admin"
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "user",
                NormalizedName = "user"
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "staff",
                NormalizedName = "staff"
            });
        }

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {

        }
    }
}