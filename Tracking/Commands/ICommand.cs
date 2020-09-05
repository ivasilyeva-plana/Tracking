using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Models;

namespace Tracking.Commands
{
    public interface ICommand
    {
        void ExecuteCommand(InputData inputData);
    }
}
