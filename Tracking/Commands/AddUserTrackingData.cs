using System;
using System.Collections.Generic;
using Tracking.DataProvider;
using Tracking.Models;
using Tracking.Properties;

namespace Tracking.Commands
{
    public class AddUserTrackingData : ICommand
    {
        
        private readonly IDataProvider _dataProvider;
        private readonly IDataReader _dataReader;


        public AddUserTrackingData(IDataProvider dataProvider, IDataReader dataReader)
        {
            _dataProvider = dataProvider;
            _dataReader = dataReader;
        }

        public void ExecuteCommand(InputData inputData)
        {
            var users = _dataReader.Read(inputData);

            _dataProvider.WithSecurityKey(inputData.Key).WriteToFile(Settings.Default.UsersFilePath, users);

            var trackingPixels = new List<TrackingModel>();
            foreach (var user in users)
            {
                trackingPixels.AddRange(user.TrackingPixels);
            }
            _dataProvider.WithSecurityKey(inputData.Key).WriteToFile(Settings.Default.TrackingFilePath, trackingPixels);
        }

    }
}
