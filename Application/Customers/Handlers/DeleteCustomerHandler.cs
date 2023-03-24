using MediatR;

using Entities.Exceptions;
using Contracts;
using Application.Customers.Commands;
using Application.Notifications;

namespace Application.Customers.Handlers;


// internal sealed class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, Unit> 
internal sealed class DeleteCustomerHandler : INotificationHandler<CustomerDeletedNotification>
{ 
    private readonly IRepositoryManager _repository; 
    public DeleteCustomerHandler(IRepositoryManager repository) => _repository = repository; 

    // public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken) 
    public async Task Handle(CustomerDeletedNotification notification, CancellationToken cancellationToken)
    { 
        //var customer = await _repository.Customer.GetCustomerAsync(request.Id, request.TrackChanges); 
        var customer = await _repository.Customer.GetCustomerAsync(notification.Id, notification.TrackChanges);
        if (customer is null) 
            //throw new CustomerNotFoundException(request.Id); 
            throw new CustomerNotFoundException(notification.Id);

        _repository.Customer.DeleteCustomer(customer);  
        await _repository.SaveAsync(); 

        // return Unit.Value; 
    } 
}