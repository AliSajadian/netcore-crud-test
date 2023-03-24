using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Entities.Models;

namespace Repository;

public class RepositoryContext : DbContext 
{ 
    public RepositoryContext(DbContextOptions options) : base(options) 
    { 

    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
            builder.Properties<DateOnly>()
                   .HaveConversion<DateOnlyConverter>()
                   .HaveColumnType("date");
            builder.Properties<DateOnly>()
                   .HaveConversion<NullableDateOnlyConverter>()
                   .HaveColumnType("date");
    } 

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    { 
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CustomerConfiguration()); 
    }

    public DbSet<Customer>? Customers { get; set; } 
}

/// <summary>
/// Converts <seecref="DateOnly" /> to <seecref="DateTime"/> and vice versa.
/// </summary>
internal class DateOnlyConverter : ValueConverter<DateOnly, DateTime>    
{
    /// <summary>
    /// Creates a new instance of this converter.
    /// </summary>
    public DateOnlyConverter() : base(
        d=>d.ToDateTime(TimeOnly.MinValue),
        d=>DateOnly.FromDateTime(d))        
    { }    
}
    
/// <summary>
/// Converts <seecref="DateOnly?" /> to <seecref="DateTime?"/> and vice versa.
/// </summary>
internal class NullableDateOnlyConverter : ValueConverter<DateOnly?, DateTime?>    
{
    /// <summary>
    /// Creates a new instance of this converter.
    /// </summary>
    public NullableDateOnlyConverter() : base(
        d=>d==null
            ?null
            :new DateTime?(d.Value.ToDateTime(TimeOnly.MinValue)),
        d=>d==null
            ?null
            :new DateOnly?(DateOnly.FromDateTime(d.Value)))        
    { }    
}