using System;
using System.Linq;
using Tracking.DataProvider;
using Tracking.Models;
using Tracking.Properties;

namespace Tracking.Commands
{
    public class PrintAllUsersTrackingData : ICommand
    {
        private readonly IDataProvider _dataProvider;
        private readonly IPresenter _presenter;

        public PrintAllUsersTrackingData(IDataProvider dataProvider, IPresenter presenter)
        {
            _dataProvider = dataProvider;
            _presenter = presenter;
        }

        public void ExecuteCommand(InputData inputData)
        {
            var users = _dataProvider.WithSecurityKey(inputData.Key).ReadFromFile<UserModel>(Settings.Default.UsersFilePath);

            var tracking = _dataProvider
                .WithSecurityKey(inputData.Key)
                .ReadFromFile<TrackingModel>(Settings.Default.TrackingFilePath);

            _presenter.PrintUserInfo(users, tracking);
        }

    }
}
