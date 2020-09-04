using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public IList<СoordinatesModel> Coordinates { get; set; }
    }
}
