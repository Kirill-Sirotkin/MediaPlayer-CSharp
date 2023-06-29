namespace MediaApp.Core;

interface ISubscriber
{
    void Receive(string message);
}