using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Tracking.Commands;
using Tracking.Managers;

namespace Tracking.Ninject
{
    public class NinjectRegistrations : NinjectModule
    {
        private readonly Models.Commands _command;

        public NinjectRegistrations(Models.Commands command) => _command = command;
        public override void Load()
        {
            Bind<IDataManager>().To<DataManager>();
            Bind<ICoder>().To<Coder>();
            switch (_command)
            {
                case Models.Commands.Add:
                    Bind<ICommand>().To<AddUserTrackingData>();
                    break;
                case Models.Commands.Read:
                    Bind<ICommand>().To<ReadUserTrackingData>();
                    break;
                case Models.Commands.Find:
                    Bind<ICommand>().To<FindUserTrackingData>();
                    break;
                default:
                    throw new Exception("Command is not recognized");
            }
        }
    }
}
