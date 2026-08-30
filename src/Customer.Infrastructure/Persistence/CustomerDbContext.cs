using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Customer.Domain.Enums;

namespace Customer.Infrastructure.Persistence
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {

        }

        public DbSet<Customer.Domain.Entities.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Entities.Customer>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.FullName)
                .HasMaxLength(200)
                .IsRequired();

                e.Property(x => x.DocumentNumber)
                  .HasMaxLength(20)
                  .IsRequired();

                e.HasIndex(x => x.DocumentNumber)
                  .IsUnique()
                  .HasDatabaseName("UQ_Customer_DocumentNumber");

                e.Property(x => x.MonthlyIncome)
                 .HasColumnType("decimal(18,2)")
                 .IsRequired();
                
                e.Property(x => x.EmploymentType)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasMaxLength(12)
                    .HasConversion<string>();
            });
        }
    }
}
