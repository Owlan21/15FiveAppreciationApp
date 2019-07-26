using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppreciationApp.Web.Models
{
    public class FifteenFive
    {
        public FifteenFive()
        {
            this.HighFives = new List<HighFives>();
        }

        public KeyValuePair<string, int> MostGiven { get; set; }

        public KeyValuePair<string, int> MostRecieved { get; set; }

        public List<HighFives> HighFives { get; set; }      
    }
}
