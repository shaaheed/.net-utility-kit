namespace Msi.UtilityKit.Sms
{
    public interface ICsvService
    {

        byte[] Generate(string text, string to);

    }
}
