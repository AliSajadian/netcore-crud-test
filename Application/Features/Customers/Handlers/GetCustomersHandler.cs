using MediatR;
using AutoMapper;

using Contracts;
using Shared.DTO;
using Application.Features.Customers.Queries;
using Shared.Responses;

namespace Application.Features.Customers.Handlers;

public sealed class GetCustomersHandler : IRequestHandler<GetCustomersQuery, MultiRecordCommandResponse> 
{ 
    private readonly IUnitOfWork _repository; 
    private readonly IMapper _mapper; 

    public GetCustomersHandler(IUnitOfWork repository, IMapper mapper)
    {
        _repository = repository; 
        _mapper = mapper; 
    }

    public async Task<MultiRecordCommandResponse> Handle(GetCustomersQuery request, CancellationToken cancellationToken) 
    { 
        var response = new MultiRecordCommandResponse();
        try
        {
            var customers = await _repository.Customer.GetAllCustomersAsync(request.TrackChanges); 
            
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers); 

            response.Success = true;
            response.Message = "Read all Successful";
            response.Customers = customersDto.ToList();
        }
        catch(Exception e)
        {
            response.Success = false;
            response.Message = "Read all Failed";
            response.Errors = new List<string>(){e.Message};   
        }
        
        return response;
    } 
}