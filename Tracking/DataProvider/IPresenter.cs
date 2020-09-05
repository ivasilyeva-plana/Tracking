using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Models;

namespace Tracking.DataProvider
{
    public interface IPresenter
    {
        void PrintMessage(string message);
        void PrintUserInfo(List<UserModel> users, List<TrackingModel> tracking);
    }
}
