using MediatR; 

using Shared.DTO;

namespace Application.Customers.Commands;


public sealed record CreateCustomerCollectionCommand(
    IEnumerable<CustomerForCreationDto> customerCollection) : IRequest<(IEnumerable<CustomerDto> customers, string ids)>;