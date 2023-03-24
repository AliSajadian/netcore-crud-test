using MediatR; 

namespace Application.Customers.Commands;


public record DeleteCustomerCommand(Guid Id, bool TrackChanges) : IRequest;