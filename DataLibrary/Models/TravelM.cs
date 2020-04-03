using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class TravelM: DataLibrary.DataAccess.Travel
    {
        // Constructor
        public TravelM()
        {
        }

        public int State { get; set; }
    }

}
