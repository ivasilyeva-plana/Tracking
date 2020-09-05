using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Managers
{
    public interface ICoder
    {
        string Encrypt(string message, int key);
        string Decrypt(string message, int key);
    }
}
