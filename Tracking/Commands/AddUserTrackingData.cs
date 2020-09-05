using System;
using System.Collections;
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
    public class AddUserTrackingData : ICommand
    {
        const string StopFillData = ":q";
        const string StopFillTracking = ":t";
        private readonly IDataManager _dataManager;

        public AddUserTrackingData(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public void ExecuteCommand(InputData inputData)
        {
            var users = InputDataReading();

            _dataManager.WriteToFile<UserModel>(Settings.Default.UsersFilePath, users);

            var trackingPixels = new List<TrackingModel>();
            foreach (var user in users)
            {
                trackingPixels.AddRange(user.TrackingPixels);
            }
            _dataManager.WriteToFile<TrackingModel>(Settings.Default.TrackingFilePath, trackingPixels);
        }

        private List<UserModel> InputDataReading()
        {
            var users = new List<UserModel>();
            var maxId = 0;
            var inputLine = Console.ReadLine();

            while (!string.Equals(inputLine, StopFillData))
            {
                var personData = inputLine?.Split(' ');

                if (personData == null || personData.Length != 3 || !int.TryParse(personData[2], out var age))
                {
                    Console.WriteLine("Error: Incorrect data format.");
                    inputLine = Console.ReadLine();
                    continue;
                }

                maxId++;
                var person = new UserModel
                {
                    Id = maxId,
                    FirstName = personData[0],
                    LastName = personData[1],
                    Age = age,
                    TrackingPixels = new List<TrackingModel>()
                };

                inputLine = Console.ReadLine();
                while (!string.Equals(inputLine, StopFillTracking))
                {
                    var data = inputLine?.Split(' ');

                    if (data == null || data.Length != 2
                                     || !float.TryParse(data[0], out var x)
                                     || !float.TryParse(data[1], out var y))
                    {
                        Console.WriteLine("Error: Incorrect data format.");
                        inputLine = Console.ReadLine();
                        continue;
                    }

                    person.TrackingPixels.Add(new TrackingModel
                    {
                        UserId = person.Id,
                        X = x,
                        Y = y
                    });
                    inputLine = Console.ReadLine();
                }
                users.Add(person);
                inputLine = Console.ReadLine();
            }

            return users;
        }

    }
}
