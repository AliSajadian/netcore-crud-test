using MediatR;
using AutoMapper;

using Entities.Exceptions;
using Contracts;
using Shared.DTO;
using Application.Customers.Queries;

namespace Application.Customers.Handlers;


sealed class GetCustomerCollectionHandler : IRequestHandler<GetCustomerCollectionQuery, IEnumerable<CustomerDto>> 
{ 
    private readonly IRepositoryManager _repository; 
    private readonly IMapper _mapper; 

    public GetCustomerCollectionHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository; 
        _mapper = mapper; 
    }

    public async Task<IEnumerable<CustomerDto>> Handle(GetCustomerCollectionQuery request, CancellationToken cancellationToken) 
    { 
        if (request.ids is null) 
            throw new IdParametersBadRequestException(); 

        var customers = await _repository.Customer.GetByIdsAsync(request.ids, request.TrackChanges); 
        
        if (request.ids.Count() != customers.Count()) 
            throw new CollectionByIdsBadRequestException(); 
        
        var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers); 
        
        return customersDto;
    } 
}