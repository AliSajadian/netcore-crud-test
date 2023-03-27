using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using AutoMapper;

using Shared.DTO;
using Contracts;
using Application.Customers.Handlers;
using Application.Customers.Queries;
using CustomerAPI;
using CrudTest.Mocks;
using Entities.Models;

namespace CustomersTest.Queries
{
    public class GetCustomerHandlerTests
    {
        private readonly ITestOutputHelper _output;
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly List<Customer> _customers;
        public GetCustomerHandlerTests(ITestOutputHelper output)
        {
            _output = output;
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            _customers = MockUnitOfWork.GetCustomers();

            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handler_Should_ReturnSuccessfulResult_When_GetAllCustomers()
        {
            //ARRANGE
            var handler = new GetCustomersHandler(_mockUnitOfWork.Object, _mapper);

            //ACT
            var result = await handler.Handle(new GetCustomersQuery(false), CancellationToken.None);

            //ASSERT
            result.Success.ShouldBe(true);
            result.Customers.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Handler_Should_ReturnSuccessfulResult_When_GetCustomerById()
        {
            //ARRANGE
            int id = 1;
            _mockUnitOfWork.Setup(r => r.Customer.GetCustomerAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var customer = _customers.AsQueryable().Where(c => c.Id == id).FirstOrDefault();
                return customer;
            });
            
            var handler = new GetCustomerHandler(_mockUnitOfWork.Object, _mapper);

            //ACT
            var result = await handler.Handle(new GetCustomerQuery(id, false), CancellationToken.None);

            //ASSERT
            result.Success.ShouldBe(true);
        }

                [Fact]
        public async Task Handler_Should_ReturnFailureResult_For_NotFound_When_GetCustomerById()
        {
            //ARRANGE
            int id = 10000;
            _mockUnitOfWork.Setup(r => r.Customer.GetCustomerAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var customer = _customers.AsQueryable().Where(c => c.Id == id).FirstOrDefault();
                return customer;
            });
            
            var handler = new GetCustomerHandler(_mockUnitOfWork.Object, _mapper);

            //ACT
            var result = await handler.Handle(new GetCustomerQuery(id, false), CancellationToken.None);

            //ASSERT
            result.Success.ShouldBe(false);
        }
    }
}
