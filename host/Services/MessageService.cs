using System.Runtime.CompilerServices;

namespace Cosmos.Samples.Service.Host.Services;

internal interface IMessageService
{
    string GetMessage(string caller = "unknown");
}

internal class MessageService() : IMessageService
{
    public string GetMessage(string caller) => $"Ping from [{caller}]\t{DateTime.Now:s}";
}