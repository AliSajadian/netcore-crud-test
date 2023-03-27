using MediatR;
using AutoMapper;

using Entities.Exceptions;
using Contracts;
using Application.Features.Customers.Commands;
using Shared.Responses;
using Shared.DTO;
using Application.Validators;

namespace Application.Features.Customers.Handlers;


public sealed class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, SingleRecordCommandResponse>
{ 
    private readonly IUnitOfWork _repository; 
    private readonly IMapper _mapper; 

    public UpdateCustomerHandler(IUnitOfWork repository, IMapper mapper) 
    { 
        _repository = repository; 
        _mapper = mapper; 
    } 

    public async Task<SingleRecordCommandResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken) 
    {
        var response = new SingleRecordCommandResponse();
        var validator = new UpdateCustomerDtoValidator();
        var validationResult = await validator.ValidateAsync(request.customer);

        var customer = await _repository.Customer.GetCustomerAsync(request.Id); 
        
        if (customer is null) 
        {
            response.Success = false;
            response.Message = "Update Failed";
            response.Errors = new List<string>(){
                $"The Customer with id: {request.Id} doesn't exist in the database."
                };
        }
        else if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Update Failed";
            response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
        }
        else
        {
            _mapper.Map(request.customer, customer); 
            await _repository.Customer.UpdateCustomerAsync(customer);
            await _repository.SaveAsync(); 
            var customerDto = _mapper.Map<CustomerDto>(customer);

            response.Success = true;
            response.Message = "Update Successful";
            response.Customer = customerDto;
            response.Id = customer.Id;
        }            

        return response; 
    } 
}