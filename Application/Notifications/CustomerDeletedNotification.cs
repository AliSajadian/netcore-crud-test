using MediatR;

namespace Application.Notifications;

public sealed record CustomerDeletedNotification(int Id, bool TrackChanges) : IRequest;