using MediatR; 

using Shared.DTO;

namespace Application.Customers.Commands;


public sealed record UpdateCustomerCommand (Guid Id, CustomerForUpdateDto customer, bool TrackChanges) : IRequest;