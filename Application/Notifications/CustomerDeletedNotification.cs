using MediatR;

namespace Application.Notifications;

public sealed record CustomerDeletedNotification(Guid Id, bool TrackChanges) : INotification;