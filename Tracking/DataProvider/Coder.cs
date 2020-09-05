using System.Linq;

namespace Tracking.DataProvider
{
    public class Coder : ICoder
    {
        public string Encrypt(string message, int key) => 
            message.Aggregate(string.Empty, (current, t) => current + (char) (t ^ key));
        
        public string Decrypt(string message, int key) => Encrypt(message, key);
    }
}
