using MediatR;
using AutoMapper;

using Contracts;
using Shared.DTO;
using Application.Customers.Queries;

namespace Application.Customers.Handlers;


sealed class GetCustomersHandler : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerDto>> 
{ 
    private readonly IRepositoryManager _repository; 
    private readonly IMapper _mapper; 

    public GetCustomersHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository; 
        _mapper = mapper; 
    }

    public async Task<IEnumerable<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken) 
    { 
        var customers = await _repository.Customer.GetAllCustomersAsync(request.TrackChanges); 
        
        var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers); 
        
        return customersDto;
    } 
}