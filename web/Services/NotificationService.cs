namespace Cosmos.Samples.Service.Web.Services;

internal interface INotificationService
{
    event Action<string>? ReceiveEvent;

    void Send(string message);
}

internal sealed class ToastNotificationService : INotificationService
{
    public event Action<string>? ReceiveEvent;

    public void Send(string message)
    {
        ReceiveEvent?.Invoke(message);
    }
}