using MediatR; 

using Shared.DTO;
using Shared.Responses;

namespace Application.Features.Customers.Commands;

public sealed record CreateCustomerCommand(CustomerForCreationDto customer) : IRequest<SingleRecordCommandResponse>;