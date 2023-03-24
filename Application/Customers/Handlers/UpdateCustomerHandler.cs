using MediatR;
using AutoMapper;

using Entities.Exceptions;
using Contracts;
using Application.Customers.Commands;

namespace Application.Customers.Handlers;


internal sealed class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Unit>
{ 
    private readonly IRepositoryManager _repository; 
    private readonly IMapper _mapper; 

    public UpdateCustomerHandler(IRepositoryManager repository, IMapper mapper) 
    { 
        _repository = repository; 
        _mapper = mapper; 
    } 

    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken) 
    {
        var customerEntity = await _repository.Customer.GetCustomerAsync(request.Id, request.TrackChanges); 
        
        if (customerEntity is null) 
            throw new CustomerNotFoundException(request.Id); 
            
        _mapper.Map(request.customer, customerEntity); 
        
        await _repository.SaveAsync(); 
        
        return Unit.Value; 
    } 
}