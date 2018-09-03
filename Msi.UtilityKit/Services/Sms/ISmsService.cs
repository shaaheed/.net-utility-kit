namespace Msi.UtilityKit.Services.Sms
{
    public interface ISmsService
    {
        void Send(string text, string to);
    }
}
