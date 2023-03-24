using MediatR; 

using Shared.DTO;

namespace Application.Customers.Queries;


public sealed record GetCustomerQuery(Guid Id, bool TrackChanges) : IRequest<CustomerDto>;