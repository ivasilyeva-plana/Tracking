using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Tracking.DataProvider
{
    public class DataProvider : IDataProvider
    {
        private int _key;
        private readonly ICoder _coder;

        public DataProvider(ICoder coder)
        {
            _coder = coder;
        }

        public IDataProvider WithSecurityKey(int key)
        {
            _key = key;
            return this;
        }

        public List<T> ReadFromFile<T>(string filePath)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(_coder.Decrypt(File.ReadAllText(filePath),_key));
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
                var y = JsonConvert.SerializeObject(list);
                var x = _coder.Encrypt(JsonConvert.SerializeObject(list), _key);
                File.WriteAllText(filePath, _coder.Encrypt(JsonConvert.SerializeObject(list),_key));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The information wasn't written to file {filePath}:{Environment.NewLine}" +
                                  $"{ex.Message}{Environment.NewLine}");
            }
        }
    }
}
