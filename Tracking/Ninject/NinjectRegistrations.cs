using Ninject.Modules;
using System;
using Tracking.Commands;
using Tracking.DataProvider;

namespace Tracking.Ninject
{
    public class NinjectRegistrations : NinjectModule
    {
        private readonly Models.Commands _command;

        public NinjectRegistrations(Models.Commands command) => _command = command;
        public override void Load()
        {
            Bind<IDataProvider>().To<DataProvider.DataProvider>();
            Bind<ICoder>().To<Coder>();
            Bind<IDataReader>().To<DataReader>();
            Bind<IPresenter>().To<Presenter>();

            switch (_command)
            {
                case Models.Commands.Add:
                    Bind<ICommand>().To<AddUserTrackingData>();
                    break;
                case Models.Commands.Read:
                    Bind<ICommand>().To<PrintAllUsersTrackingData>();
                    break;
                case Models.Commands.Find:
                    Bind<ICommand>().To<PrintUserTrackingData>();
                    break;
                default:
                    throw new Exception("Command is not recognized");
            }
        }
    }
}
