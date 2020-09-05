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

        public ReadUserTrackingData(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public void ExecuteCommand(InputData inputData)
        {
            var users = _dataManager.ReadFromFile<UserModel>(Settings.Default.UsersFilePath);
            var tracking = _dataManager.ReadFromFile<TrackingModel>(Settings.Default.TrackingFilePath);

            foreach (var user in users)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName} {user.Age}");
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
