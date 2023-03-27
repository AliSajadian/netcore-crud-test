using MediatR;

using Contracts;
using Application.Features.Customers.Commands;

namespace Application.Features.Customers.Handlers;


public sealed class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, NoneRecordCommandResponse>
{ 
    private readonly IUnitOfWork _repository; 
    public DeleteCustomerHandler(IUnitOfWork repository) => _repository = repository; 

    public async Task<NoneRecordCommandResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    { 
        var response = new NoneRecordCommandResponse();

        var customer = await _repository.Customer.GetCustomerAsync(request.Id);

        if (customer is null) 
        {
            response.Success = false;
            response.Message = "Delete Failed";
            response.Errors = new List<string>(){
                $"The Customer with id: {request.Id} doesn't exist in the database."
                };
        }
        else
        {
            await _repository.Customer.DeleteCustomerAsync(customer);  

            await _repository.SaveAsync();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = customer.Id;
        }            

        return response; 
    } 
}