using KidAdvisor.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KidAdvisor
{
    public class BusinessContext : DbContext
    {
        private IHttpContextAccessor _httpContextAccessor;
        public BusinessContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Business> Businesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var firstUserId = new Guid("9f1c7846-19c5-42e7-b2c6-265934a42214");
            var creatorId = new Guid("1939f2b1-6eb4-4c46-87a9-9e7de111a789");
            var businessId = new Guid("3fded86a-2f50-4c7d-8403-81f58bfed969");

            modelBuilder.Entity<User>(ent =>
            {
                ent.Property(d => d.UserNumber).ValueGeneratedOnAdd().IsRequired(true);
                ent.Property(d => d.FirstName).IsRequired().HasMaxLength(50);
                ent.Property(d => d.LastName).IsRequired().HasMaxLength(50);

                ent.HasData(new List<User>()
                {
                    new User()
                    {
                        UserId = firstUserId,
                        FirstName = "Mike",
                        LastName = "Tyson",
                        DateCreated = DateTime.Now,
                        LastEditDate = DateTime.Now,
                        RecordCreatorId = creatorId,
                        LastEditorId = creatorId
                    }
                });
            });

            modelBuilder.Entity<Business>(ent =>
            {
                ent.Property(d => d.BusinessNumber).ValueGeneratedOnAdd().IsRequired(true);
                ent.Property(d => d.Name).IsRequired().HasMaxLength(100);
                ent.Property(d => d.StreetAddress).IsRequired().HasMaxLength(256);
                ent.Property(d => d.City).IsRequired().HasMaxLength(50);
                ent.Property(d => d.PostalCode).IsRequired().HasMaxLength(50);
                ent.Property(d => d.Country).IsRequired().HasMaxLength(50);
                ent.Property(d => d.Description).HasMaxLength(2000);
                ent.Property(d => d.Appartement).HasMaxLength(50);
                ent.Property(d => d.Province).HasMaxLength(50);

                ent.HasOne(c => c.Owner)
                    .WithMany(e => e.Businesses)
                    .HasForeignKey(c => c.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);

                ent.HasData(new List<Business>() {
                    new Business
                    {
                        BusinessId = businessId,
                        Name = "Pizza Hut",
                        StreetAddress = "6135 Hyatt Trail Suit",
                        City = "NewYork",
                        PostalCode = "5000",
                        Country = "USA",
                        Appartement = "15F",
                        OwnerId = firstUserId,
                        DateCreated = DateTime.Now,
                        LastEditDate = DateTime.Now,
                        RecordCreatorId = Guid.Empty,
                        LastEditorId = Guid.Empty
                    }
                });
            });


            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity)
                {
                    var trackable = entry.Entity as BaseEntity;
                    var now = DateTime.UtcNow;
                    var userId = GetCurrentUser();

                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.LastEditDate = now;
                            trackable.LastEditorId = userId;
                            break;

                        case EntityState.Added:
                            trackable.DateCreated = now;
                            trackable.RecordCreatorId = userId;
                            trackable.LastEditDate = now;
                            trackable.LastEditorId = userId;
                            break;
                    }
                }
            }
        }

        private Guid GetCurrentUser()
        {
            var result = Guid.Empty;

            //var nameIdentifier = this._httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //var userData = this._httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.UserData)?.Value;

            return result;
        }
    }
}
