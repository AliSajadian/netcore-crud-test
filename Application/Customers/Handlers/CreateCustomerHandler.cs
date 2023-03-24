using MediatR;
using AutoMapper;

using Entities.Models;
using Contracts;
using Shared.DTO;
using Application.Customers.Commands;

namespace Application.Customers.Handlers;


internal sealed class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerDto> 
{ 
    private readonly IRepositoryManager _repository; 
    private readonly IMapper _mapper; 

    public CreateCustomerHandler(IRepositoryManager repository, IMapper mapper) 
    { 
        _repository = repository; 
        _mapper = mapper; 
    } 
    
    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken) 
    { 
        var customerEntity = _mapper.Map<Customer>(request.customer); 
        _repository.Customer.CreateCustomer(customerEntity); 
        await _repository.SaveAsync();

        var customerToReturn = _mapper.Map<CustomerDto>(customerEntity); 
        return customerToReturn; 
    } 
}