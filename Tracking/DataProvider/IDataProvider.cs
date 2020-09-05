using System.Collections.Generic;

namespace Tracking.DataProvider
{
    public interface IDataProvider
    {
        IDataProvider WithSecurityKey(int key);
        List<T> ReadFromFile<T>(string filePath);
        void WriteToFile<T>(string filePath, List<T> list);

    }
}
