namespace MediaApp.Core;

interface IEmitter
{
    void Subscribe(ISubscriber subscriber);
    void Unsubscribe(ISubscriber subscriber);
    void Notify(string message);
}