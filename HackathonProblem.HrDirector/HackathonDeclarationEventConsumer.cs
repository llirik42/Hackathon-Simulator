using HackathonProblem.Common.models.events;
using MassTransit;

namespace HackathonProblem.HrDirector;

public class HackathonDeclarationEventConsumer : IConsumer<HackathonDeclarationEvent>
{
    public Task Consume(ConsumeContext<HackathonDeclarationEvent> context)
    {
        Console.WriteLine(context.Message.HackathonId);
        return Task.CompletedTask;
    }
}
