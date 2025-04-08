using Family.Contracts;
using MassTransit;

namespace Family.Application.Services;

public class PublishService : IPublishService
{
    private readonly IPublishEndpoint _familyPublishEndpoint;

    public PublishService(IPublishEndpoint familyPublishEndpoint)
    {
        _familyPublishEndpoint = familyPublishEndpoint;
    }

    public async Task PublishAsync(IContractMessage message)
    {
        await _familyPublishEndpoint.Publish(message);
    }
}
