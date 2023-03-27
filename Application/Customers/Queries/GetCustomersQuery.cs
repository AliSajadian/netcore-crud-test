using MediatR; 

using Shared.DTO;
using Shared.Responses;

namespace Application.Customers.Queries;

public sealed record GetCustomersQuery(bool TrackChanges) : IRequest<MultiRecordCommandResponse>;