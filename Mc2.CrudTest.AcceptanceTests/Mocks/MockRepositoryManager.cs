using System.Linq;
using Moq;
using System;
using System.Collections.Generic;

using Contracts;
using Entities.Models;

namespace CrudTest.Mocks
{
    public static class MockUnitOfWork
    {
        private static List<Customer> Customers;
        public static List<Customer> GetCustomers()
        {
            Customers = new List<Customer>
            {
                new Customer{
                    Id = 1, 
                    FirstName = "Tom", 
                    LastName = "Hangs", 
                    DateOfBirth = new DateTime(1956,06,09), 
                    PhoneNumber = "+461532895412",
                    Email = "Tom.Hangs@gmail.com",
                BankAccountNumber = "3453763731234523452346"
                },
                new Customer{
                    Id = 2, 
                    FirstName = "Harrison", 
                    LastName = "Ford", 
                    DateOfBirth = new DateTime(1942,06,13), 
                    PhoneNumber = "+466432895745",
                    Email = "Harrison.Ford@gmail.com",
                    BankAccountNumber = "8431785581235190054864"
                },
            };
            return Customers;
        }        
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.SetupSequence(r => r.Customer.GetAllCustomersAsync(false)).ReturnsAsync(Customers);

            return mockRepo;
        }
    }
}
