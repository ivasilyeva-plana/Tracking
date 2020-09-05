using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Managers
{
    public class Coder : ICoder
    {
        public string Encrypt(string message, int key) => 
            message.Aggregate(string.Empty, (current, t) => current + (char) (t ^ key));
        
        public string Decrypt(string message, int key) => Encrypt(message, key);
    }
}
