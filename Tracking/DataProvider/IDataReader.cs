using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Models;

namespace Tracking.DataProvider
{
    public interface IDataReader
    {
        List<UserModel> Read(InputData inputData);
    }
}
