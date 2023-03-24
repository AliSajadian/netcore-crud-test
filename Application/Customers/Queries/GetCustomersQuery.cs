using MediatR; 

using Shared.DTO;

namespace Application.Customers.Queries;

public sealed record GetCustomersQuery(bool TrackChanges) : IRequest<IEnumerable<CustomerDto>>;