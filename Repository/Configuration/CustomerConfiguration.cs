using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Entities.Models;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer> 
{ 
    public void Configure(EntityTypeBuilder<Customer> builder)
    { 
        builder.HasData ( 
            new Customer 
            { 
                Id = 1, 
                FirstName = "Tom", 
                LastName = "Hangs", 
                DateOfBirth = new DateTime(1956,06,09), 
                PhoneNumber = "+461532895412",
                Email = "Tom.Hangs@gmail.com",
                BankAccountNumber = "3453763731234523452346"
            }, 
            new Customer
            {
                Id = 2, 
                FirstName = "Harrison", 
                LastName = "Ford", 
                DateOfBirth = new DateTime(1942,06,13), 
                PhoneNumber = "+466432895745",
                Email = "Harrison.Ford@gmail.com",
                BankAccountNumber = "8431785581235190054864"
            }
        ); 
    } 
}