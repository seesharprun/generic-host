using Cosmos.Samples.Service.Host.Settings;
using Microsoft.Extensions.Options;

namespace Cosmos.Samples.Service.Host.Services;

internal interface IMessageService
{
    string GetMessage();
}

internal class MessageService(IOptions<Messages> messageOptions) : IMessageService
{
    private readonly Messages _messages = messageOptions.Value;

    public string GetMessage() => $"{_messages.Greeting}{Environment.NewLine}This was generated at {DateTime.Now:R}!";
}