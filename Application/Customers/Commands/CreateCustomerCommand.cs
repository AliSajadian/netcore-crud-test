using MediatR; 

using Shared.DTO;

namespace Application.Customers.Commands;


public sealed record CreateCustomerCommand(CustomerForCreationDto customer) : IRequest<CustomerDto>;