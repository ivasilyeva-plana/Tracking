using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tracking.Managers;
using Tracking.Models;
using Tracking.Properties;

namespace Tracking.Commands
{
    public class ReadUserTrackingData : ICommand
    {
        private readonly IDataManager _dataManager;
        private readonly ICoder _coder;

        public ReadUserTrackingData(IDataManager dataManager, ICoder coder)
        {
            _dataManager = dataManager;
            _coder = coder;
        }

        public void ExecuteCommand(InputData inputData)
        {
            var users = _dataManager.ReadFromFile<UserModel>(Settings.Default.UsersFilePath);

            var tracking = _dataManager.ReadFromFile<TrackingModel>(Settings.Default.TrackingFilePath);
            foreach (var user in users)
            {
                Console.WriteLine($"{_coder.Decrypt(user.FirstName, inputData.Key)} {_coder.Decrypt(user.LastName,inputData.Key)} {user.Age}");
                var userTracking = tracking.Where(i => i.UserId == user.Id);
                foreach (var item in tracking)
                {
                    Console.WriteLine($"{item.X} {item.Y}");
                }
            }
            Console.ReadKey();
        }

    }
}
