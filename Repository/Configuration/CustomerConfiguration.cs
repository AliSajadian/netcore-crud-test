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
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 
                FirstName = "Tom", 
                LastName = "Hangs", 
                DateOfBirth = new DateOnly(1956,06,09), 
                PhoneNumber = "+461532895412",
                Email = "Tom.Hangs@gmail.com",
                BankAccountNumber = "3453763731234523452346"
            }, 
            new Customer
            {
                Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), 
                FirstName = "Harrison", 
                LastName = "Ford", 
                DateOfBirth = new DateOnly(1942,06,13), 
                PhoneNumber = "+466432895745",
                Email = "Harrison.Ford@gmail.com",
                BankAccountNumber = "8431785581235190054864"
            }
        ); 
    } 
}