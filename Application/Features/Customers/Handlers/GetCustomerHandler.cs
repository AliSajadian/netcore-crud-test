using MediatR;
using AutoMapper;

using Entities.Exceptions;
using Contracts;
using Shared.DTO;
using Application.Features.Customers.Queries;
using Shared.Responses;

namespace Application.Features.Customers.Handlers;


public sealed class GetCustomerHandler : IRequestHandler<GetCustomerQuery, SingleRecordCommandResponse> 
{ 
    private readonly IUnitOfWork _repository; 
    private readonly IMapper _mapper; 

    public GetCustomerHandler(IUnitOfWork repository, IMapper mapper) 
    { 
        _repository = repository; _mapper = mapper; 
    } 
    
    public async Task<SingleRecordCommandResponse> Handle(GetCustomerQuery request, CancellationToken cancellationToken) 
    { 
        var response = new SingleRecordCommandResponse();
        var customer = await _repository.Customer.GetCustomerAsync(request.Id); 
        
        if (customer is null) 
        {
            response.Success = false;
            response.Message = "Read Failed";
            response.Errors = new List<string>(){
                $"The Customer with id: {request.Id} doesn't exist in the database."
            }; 
        }
        else
        {
            var customerDto = _mapper.Map<CustomerDto>(customer); 

            response.Success = true;
            response.Message = "Read Successful";
            response.Customer = customerDto;
        }            
        
        return response;
    }
}