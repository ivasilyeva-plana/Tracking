using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Models;

namespace Tracking.DataProvider
{
    public class Presenter : IPresenter
    {
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }

        public void PrintUserInfo(List<UserModel> users, List<TrackingModel> tracking)
        {
            foreach (var user in users)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName} {user.Age}");
                var userTracking = tracking.Where(i => i.UserId == user.Id);
                foreach (var item in userTracking)
                {
                    Console.WriteLine($"{item.X} {item.Y}");
                }
            }
            Console.ReadKey();
        }
    }
}
