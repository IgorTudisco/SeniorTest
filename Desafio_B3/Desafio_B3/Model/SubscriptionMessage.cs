namespace Desafio_B3.Model;

public class SubscriptionMessage
{
    public string Event { get; set; }
    public SubscriptionData Data { get; set; }

    public SubscriptionMessage(string eventName, string channelName)
    {
        Event = eventName;
        Data = new SubscriptionData { Channel = channelName };
    }
}

public class SubscriptionData
{
    public string Channel { get; set; }
}
