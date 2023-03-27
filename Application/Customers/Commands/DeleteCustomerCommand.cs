using MediatR;
using Shared.Responses;

namespace Application.Customers.Commands;


public record DeleteCustomerCommand(int Id, bool TrackChanges) : IRequest<NoneRecordCommandResponse>;