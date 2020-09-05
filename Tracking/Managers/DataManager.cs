using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tracking.Managers
{
    public class DataManager : IDataManager
    {
        public List<T> ReadFromFile<T>(string filePath)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}{Environment.NewLine}");
                Console.ReadKey();
                return default;
            }
        }

        public void WriteToFile<T>(string filePath, List<T> list)
        {
            try
            {
                File.WriteAllText(filePath, JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The information wasn't written to file {filePath}:{Environment.NewLine}" +
                                  $"{ex.Message}{Environment.NewLine}");
            }
        }
    }
}
