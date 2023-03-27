using MediatR; 

using Shared.DTO;
using Shared.Responses;

namespace Application.Features.Customers.Queries;

public sealed record GetCustomersQuery(bool TrackChanges) : IRequest<MultiRecordCommandResponse>;