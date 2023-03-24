using MediatR;
using AutoMapper;

using Entities.Models;
using Entities.Exceptions;
using Contracts;
using Shared.DTO;
using Application.Customers.Commands;

namespace Application.Customers.Handlers;


internal sealed class CreateCustomerCollectionHandler : 
    IRequestHandler<CreateCustomerCollectionCommand, (IEnumerable<CustomerDto> customers, string ids)> 
{ 
    private readonly IRepositoryManager _repository; 
    private readonly IMapper _mapper; 

    public CreateCustomerCollectionHandler(IRepositoryManager repository, IMapper mapper) 
    { 
        _repository = repository; 
        _mapper = mapper; 
    } 
    
    public async Task<(IEnumerable<CustomerDto> customers, string ids)> Handle(
        CreateCustomerCollectionCommand request, CancellationToken cancellationToken) 
    { 
        if (request.customerCollection is null) 
            throw new CustomerCollectionBadRequest(); 

        var customerEntities = _mapper.Map<IEnumerable<Customer>>(request.customerCollection); 
        foreach (var customer in customerEntities) 
        { 
            _repository.Customer.CreateCustomer(customer); 
        } 
        await _repository.SaveAsync();

        var customerCollectionToReturn = _mapper.Map<IEnumerable<CustomerDto>>(customerEntities); 
        var ids = string.Join(",", customerCollectionToReturn.Select(c => c.Id)); 

        return (customers: customerCollectionToReturn, ids: ids); 
    } 
}