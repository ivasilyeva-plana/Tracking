using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Models;

namespace Tracking.DataProvider
{
    public class DataReader : IDataReader
    {
        const string StopFillData = ":q";
        const string StopFillTracking = ":t";
        public List<UserModel> Read(InputData inputData)
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
