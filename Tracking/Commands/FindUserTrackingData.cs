using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tracking.Managers;
using Tracking.Models;
using Tracking.Properties;

namespace Tracking.Commands
{
    public class FindUserTrackingData : ICommand
    {
        private readonly IDataManager _dataManager;
        private readonly ICoder _coder;

        public FindUserTrackingData(IDataManager dataManager, ICoder coder)
        {
            _dataManager = dataManager;
            _coder = coder;
        }
        public void ExecuteCommand(InputData inputData)
        {
            var users = _dataManager.ReadFromFile<UserModel>(Settings.Default.UsersFilePath);
            var  regex = new Regex($"{inputData.Parameters[0].Replace("*", @"\S*")}");
            var matchUsers = users.Where(i => regex.IsMatch($"{i.FirstName} {i.LastName}")).ToList();
            if (!matchUsers.Any())
            {
                Console.WriteLine($"Not found");
                Console.ReadKey();
                return;
            }

            var tracking = _dataManager.ReadFromFile<TrackingModel>(Settings.Default.TrackingFilePath);
            foreach (var user in matchUsers)
            {
                Console.WriteLine($"{_coder.Decrypt(user.FirstName, inputData.Key)} {_coder.Decrypt(user.LastName, inputData.Key)} {user.Age}");
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
