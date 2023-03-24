using MediatR; 

using Shared.DTO;

namespace Application.Customers.Queries;

public sealed record GetCustomerCollectionQuery(IEnumerable<Guid> ids, 
        bool TrackChanges) : IRequest<IEnumerable<CustomerDto>>;