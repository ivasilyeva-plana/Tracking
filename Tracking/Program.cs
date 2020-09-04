using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Tracking.Commands;
using Tracking.Models;
using Tracking.Ninject;

namespace Tracking
{
    class Program
    {
        static void Main(string[] args)
        {
            InputData inputData;
            try
            {
                inputData = InputData.Parse(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }

            var registrations = new NinjectRegistrations(inputData.CommandValue);
            var kernel = new StandardKernel(registrations);

            var task = StartOperationAsync(inputData,  kernel);
        }

        private static async Task StartOperationAsync(InputData inputData, IKernel kernel)
        {
            var query = kernel.Get<ICommand>();
            await query.ExecuteCommandAsync(inputData);
        }
    }
}
