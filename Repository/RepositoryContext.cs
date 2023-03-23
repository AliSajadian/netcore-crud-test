using Microsoft.EntityFrameworkCore;

using Entities.Models;

namespace Repository;

public class RepositoryContext : DbContext 
{ 
    public RepositoryContext(DbContextOptions options) : base(options) 
    { 

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    { 
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CustomerConfiguration()); 
    }

    public DbSet<Customer>? Customers { get; set; } 
}