using MediatR; 

using Shared.DTO;
using Shared.Responses;

namespace Application.Customers.Queries;


public sealed record GetCustomerQuery(int Id, bool TrackChanges) : IRequest<SingleRecordCommandResponse>;