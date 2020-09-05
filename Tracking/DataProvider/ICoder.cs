namespace Tracking.DataProvider
{
    public interface ICoder
    {
        string Encrypt(string message, int key);
        string Decrypt(string message, int key);
    }
}
