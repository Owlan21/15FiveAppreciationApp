using AppreciationApp.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppreciationApp.Web.Models
{
    public class AppreciationViewModel
    {
        public AppreciationViewModel()
        {
            this.HighFives = new List<HighFivesViewModel>();
        }

        /// <summary>
        /// Gets or sets the most given.
        /// </summary>
        /// <value>
        /// The most given.
        /// </value>
        public KeyValuePair<string, int> MostGiven { get; set; }

        /// <summary>
        /// Gets or sets the most recieved.
        /// </summary>
        /// <value>
        /// The most recieved.
        /// </value>
        public KeyValuePair<string, int> MostRecieved { get; set; }

        /// <summary>
        /// Gets or sets the high fives.
        /// </summary>
        /// <value>
        /// The high fives.
        /// </value>
        public List<HighFivesViewModel> HighFives { get; set; }
    }
}
