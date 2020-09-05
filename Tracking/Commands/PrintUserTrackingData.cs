using System;
using System.Linq;
using System.Text.RegularExpressions;
using Tracking.DataProvider;
using Tracking.Models;
using Tracking.Properties;

namespace Tracking.Commands
{
    public class PrintUserTrackingData : ICommand
    {
        private readonly IDataProvider _dataProvider;
        private readonly IPresenter _presenter;

        public PrintUserTrackingData(IDataProvider dataProvider, IPresenter presenter)
        {
            _dataProvider = dataProvider;
            _presenter = presenter;
        }
        public void ExecuteCommand(InputData inputData)
        {
            var users = _dataProvider.WithSecurityKey(inputData.Key).ReadFromFile<UserModel>(Settings.Default.UsersFilePath);
            var regex = GetRegex(inputData.Parameters[0]);
            var matchUsers = users.Where(i => regex.IsMatch($"{i.FirstName} {i.LastName}")).ToList();
            if (!matchUsers.Any())
            {
                _presenter.PrintMessage("Not found");
                return;
            }

            var tracking = _dataProvider.WithSecurityKey(inputData.Key).ReadFromFile<TrackingModel>(Settings.Default.TrackingFilePath);
            _presenter.PrintUserInfo(matchUsers, tracking);
        }

        private Regex GetRegex(string pattern) => new Regex($"^{pattern.Replace("*", @".*")}$");
        
    }
}
