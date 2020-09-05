using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Managers
{
    public interface IDataManager
    {
        List<T> ReadFromFile<T>(string filePath);
        void WriteToFile<T>(string filePath, List<T> list);

    }
}
