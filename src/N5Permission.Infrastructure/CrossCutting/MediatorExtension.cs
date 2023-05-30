using MediatR;
using N5Permission.Domain.SeedWork;
using N5Permission.Infrastructure.Persistance;

namespace N5Permission.Infrastructure.CrossCutting;

static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, N5PermissionContext context)
    {
        var domainEntities = context.ChangeTracker
            .Entries<EntityEvent>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
