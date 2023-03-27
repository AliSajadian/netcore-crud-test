using MediatR; 

using Shared.Responses;

namespace Application.Features.Customers.Queries;

public sealed record GetCustomerQuery(int Id, bool TrackChanges) : IRequest<SingleRecordCommandResponse>;