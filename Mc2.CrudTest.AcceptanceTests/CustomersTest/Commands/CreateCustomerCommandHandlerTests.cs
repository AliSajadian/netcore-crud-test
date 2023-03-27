using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using AutoMapper;

using Contracts;
using Shared.Responses;
using Shared.DTO;
using Application.Customers.Commands;
using Application.Customers.Handlers;
using CrudTest.Mocks;
using Entities.Models;
using Application.Notifications;
using System.Collections.Generic;

namespace CustomersTest.Commands
{
    public class CreateCustomerCommandHandlerTests
    {
        private readonly ITestOutputHelper _output;
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private List<Customer> _customers;

        private readonly CustomerForCreationDto _validCustomerForCreationDto;
        private readonly CustomerForCreationDto _invalidCustomerForCreationDto;
        private readonly CustomerForUpdateDto _validCustomerForUpdateDto;
        private readonly CustomerForUpdateDto _invalidCustomerForUpdateDto;

        public CreateCustomerCommandHandlerTests(ITestOutputHelper output)
        {
            //ARRANGE
            _output = output;
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            _customers = MockUnitOfWork.GetCustomers();
            
            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _validCustomerForCreationDto = new CustomerForCreationDto
            {
                FirstName = "Robert", 
                LastName = "De Niro", 
                DateOfBirth = new DateTime(1943,08,17), 
                PhoneNumber = "+5938648525",
                Email = "deniro.robert@gmail.com",
                BankAccountNumber = "5599468233357771211094"
            };
            _invalidCustomerForCreationDto = new CustomerForCreationDto
            {
                FirstName = null, 
                LastName = "De Niro", 
                DateOfBirth = new DateTime(1943,08,17), 
                PhoneNumber = "+5938648525",
                Email = "deniro.robert@gmail.com",
                BankAccountNumber = "5599468233357771211094"
            };
            _validCustomerForUpdateDto = new CustomerForUpdateDto
            {
                FirstName = "Tom", 
                LastName = "Hangs", 
                DateOfBirth = new DateTime(1956,03,09), 
                PhoneNumber = "+7777777777",
                Email = "hangs.tom@gmail.com",
                BankAccountNumber = "777777777777777777777777"
            };
            _invalidCustomerForUpdateDto = new CustomerForUpdateDto
            {
                FirstName = null, 
                LastName = "hangs", 
                DateOfBirth = new DateTime(1956,03,09), 
                PhoneNumber = "+7777777777",
                Email = "hangs.tom@gmail.com",
                BankAccountNumber = "777777777777777777777777"
            };
        }

        [Fact]
        public async Task Handler_Should_ReturnSuccessfulResult_When_CreateCustomerByValidEntity()
        {
            //ARRANGE
            _mockUnitOfWork.Setup(r => r.Customer.CreateCustomerAsync(It.IsAny<Customer>())).ReturnsAsync((Customer customer) => 
            {
                _customers.Add(customer);
                return customer;
            });
            var _handler = new CreateCustomerHandler(_mockUnitOfWork.Object, _mapper);

            //ACT
            var result = await _handler.Handle(new CreateCustomerCommand(_validCustomerForCreationDto), CancellationToken.None);

            //ASSERT
            result.ShouldBeOfType<SingleRecordCommandResponse>();
            result.Success.ShouldBe<bool>(true);

            _customers.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handler_Should_ReturnFailureResult_For_BadRequest_When_CreateCustomerByInvalidEntity()
        {
            //ARRANGE
            _mockUnitOfWork.Setup(r => r.Customer.CreateCustomerAsync(It.IsAny<Customer>())).ReturnsAsync((Customer customer) => 
            {
                _customers.Add(customer);
                return customer;
            });
            var _handler = new CreateCustomerHandler(_mockUnitOfWork.Object, _mapper);

            //ACT
            var result = await _handler.Handle(new CreateCustomerCommand(_invalidCustomerForCreationDto), CancellationToken.None);

            //ASSERT
            result.ShouldBeOfType<SingleRecordCommandResponse>();
            result.Success.ShouldBe<bool>(false);

            _customers.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Handler_Should_ReturnSuccessfulResult_When_UpdateCustomerByValidEntity()
        {
            //ARRANGE
            int id = 1;
            _mockUnitOfWork.Setup(r => r.Customer.GetCustomerAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var customer = _customers.AsQueryable().Where(c => c.Id == id).FirstOrDefault();
                return customer;
            });
            var _handler = new UpdateCustomerHandler(_mockUnitOfWork.Object, _mapper);

            //ACT
            var result = await _handler.Handle(new UpdateCustomerCommand(id, _validCustomerForUpdateDto, false), CancellationToken.None);

            //ASSERT
            result.ShouldBeOfType<SingleRecordCommandResponse>();

            result.Success.ShouldBe(true);
        }

        [Fact]
        public async Task Handler_Should_ReturnFailureResult_For_NotFound_When_UpdateCustomerByValidEntity()
        {
            //ARRANGE
            int id = 10000;
            _mockUnitOfWork.Setup(r => r.Customer.GetCustomerAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var customer = _customers.AsQueryable().Where(c => c.Id == id).FirstOrDefault();
                return customer;
            });
            var _handler = new UpdateCustomerHandler(_mockUnitOfWork.Object, _mapper);

            //ACT
            var result = await _handler.Handle(new UpdateCustomerCommand(id, _validCustomerForUpdateDto, false), CancellationToken.None);

            //ASSERT
            result.ShouldBeOfType<SingleRecordCommandResponse>();

            result.Success.ShouldBe(false);
        }

        [Fact]
        public async Task Handler_Should_ReturnFailureResult_For_BadRequest_When_UpdateCustomerByInvalidEntity()
        {
            //ARRANGE
            int id = 1;
            _mockUnitOfWork.Setup(r => r.Customer.GetCustomerAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var customer = _customers.AsQueryable().Where(c => c.Id == id).FirstOrDefault();
                return customer;
            });
            var _handler = new UpdateCustomerHandler(_mockUnitOfWork.Object, _mapper);

            //ACT
            var result = await _handler.Handle(new UpdateCustomerCommand(id, _invalidCustomerForUpdateDto, false), CancellationToken.None);

            //ASSERT
            result.ShouldBeOfType<SingleRecordCommandResponse>();

            result.Success.ShouldBe(false);
        }
    
        [Fact]
        public async Task Handler_Should_ReturnSuccessfulResult_When_DeleteCustomerByValidEntity()
        {
            //ARRANGE
            int id = 1;
            _mockUnitOfWork.Setup(r => r.Customer.GetCustomerAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var customer = _customers.AsQueryable().Where(c => c.Id == id).FirstOrDefault();
                return customer;
            });
            _mockUnitOfWork.Setup(r => r.Customer.DeleteCustomerAsync(It.IsAny<Customer>())).Callback((Customer customer) => {
                _customers = _customers.AsQueryable().Where(c => c.Id != customer.Id).ToList();
            });

            var _handler = new DeleteCustomerHandler(_mockUnitOfWork.Object);

            //ACT
            var result = await _handler.Handle(new DeleteCustomerCommand(id, false), CancellationToken.None);

            //ASSERT
            result.ShouldBeOfType<NoneRecordCommandResponse>();

            result.Success.ShouldBe(true);

            _customers.Count.ShouldBe(1);
        }

        [Fact]
        public async Task Handler_Should_ReturnFailureResult_When_DeleteCustomerByInvalidEntity()
        {
            //ARRANGE
            int id = 10000;
            _mockUnitOfWork.Setup(r => r.Customer.GetCustomerAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var customer = _customers.AsQueryable().Where(c => c.Id == id).FirstOrDefault();
                return customer;
            });
            _mockUnitOfWork.Setup(r => r.Customer.DeleteCustomerAsync(It.IsAny<Customer>())).Callback((Customer customer) => {
                _customers = _customers.AsQueryable().Where(c => c.Id != customer.Id).ToList();
            });
            var _handler = new DeleteCustomerHandler(_mockUnitOfWork.Object);

            //ACT
            var result = await _handler.Handle(new DeleteCustomerCommand(id, false), CancellationToken.None);

            //ASSERT
            result.ShouldBeOfType<NoneRecordCommandResponse>();

            result.Success.ShouldBe(false);

            _customers.Count.ShouldBe(2);
        }
    
    }
}
