using MediatR; 

using Shared.DTO;
using Shared.Responses;

namespace Application.Features.Customers.Commands;


public sealed record UpdateCustomerCommand (int Id, CustomerForUpdateDto customer, bool TrackChanges) : IRequest<SingleRecordCommandResponse>;