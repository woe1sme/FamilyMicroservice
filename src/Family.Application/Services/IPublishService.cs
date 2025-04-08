using Family.Contracts;

namespace Family.Application.Services;

public interface IPublishService
{
    /// <summary>
    /// Publish new message to exchange
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task PublishAsync(IContractMessage message);
}