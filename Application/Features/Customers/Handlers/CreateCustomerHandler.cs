using MediatR;
using AutoMapper;

using Entities.Models;
using Contracts;
using Shared.DTO;
using Shared.Responses;
using Application.Features.Customers.Commands;
using Application.Validators;

namespace Application.Features.Customers.Handlers;

public sealed class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, SingleRecordCommandResponse> 
{ 
    private readonly IUnitOfWork _repository; 
    private readonly IMapper _mapper; 

    public CreateCustomerHandler(IUnitOfWork repository, IMapper mapper) 
    { 
        _repository = repository; 
        _mapper = mapper; 
    } 
    
    public async Task<SingleRecordCommandResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken) 
    { 
        var response = new SingleRecordCommandResponse();
        var validator = new CreateCustomerDtoValidator();
        var validationResult = await validator.ValidateAsync(request.customer);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Creation Failed";
            response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
        }
        else
        {
            var customer = _mapper.Map<Customer>(request.customer); 
            customer = await _repository.Customer.CreateCustomerAsync(customer); 
            await _repository.SaveAsync();
            var customerDto = _mapper.Map<CustomerDto>(customer);

            response.Success = true;
            response.Message = "Creation Successful";
            response.Customer = customerDto;
            response.Id = customer.Id;
        }
        return response; 
    } 
}