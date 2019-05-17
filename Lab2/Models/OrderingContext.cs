using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Lab2.Models
{
    using Microsoft.EntityFrameworkCore;
    
    
        public partial class OrderingContext : IdentityDbContext<User>
        {
            #region Constructor
            public OrderingContext(DbContextOptions<OrderingContext>
            options)
            : base(options)
            { }
            #endregion
            public virtual DbSet<Order> Order { get; set; }
            public virtual DbSet<Product> Product { get; set; }
            public virtual DbSet<User> User { get; set; }
            public virtual DbSet<OrderString> OrderString { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>(entity =>
                {
                    entity.Property(e => e.Date).IsRequired();
                    entity.Property(e => e.Act).IsRequired();
                    entity.Property(e => e.Sum).IsRequired();
                    entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId);
                });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Costs).IsRequired();
                
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserName).IsRequired();

            });
            modelBuilder.Entity<OrderString>(entity =>
                {
                    entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderString)
                    .HasForeignKey(d => d.OrderId);

                    entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderString)
                    .HasForeignKey(d => d.ProductId);
                });



            }
        }
    
}
