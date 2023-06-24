namespace MediaApp;

interface ISubscriber
{
    void Receive(string message);
}