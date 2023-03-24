using MediatR;
using AutoMapper;

using Entities.Exceptions;
using Contracts;
using Shared.DTO;
using Application.Customers.Queries;

namespace Application.Customers.Handlers;


internal sealed class GetCustomerHandler : IRequestHandler<GetCustomerQuery, CustomerDto> 
{ 
    private readonly IRepositoryManager _repository; 
    private readonly IMapper _mapper; 

    public GetCustomerHandler(IRepositoryManager repository, IMapper mapper) 
    { 
        _repository = repository; _mapper = mapper; 
    } 
    
    public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken) 
    { 
        var customer = await _repository.Customer.GetCustomerAsync(request.Id, request.TrackChanges); 
        
        if (customer is null) 
            throw new CustomerNotFoundException(request.Id); 
            
        var customerDto = _mapper.Map<CustomerDto>(customer); 
        
        return customerDto;
    }
}