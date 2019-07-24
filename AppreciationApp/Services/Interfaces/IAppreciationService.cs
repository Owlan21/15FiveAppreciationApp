using AppreciationApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppreciationApp.Web.Services.Interfaces
{
    public interface IAppreciationService
    {
        FifteenFive GetAppreciations();
        string SpaceApartNames(string message);

        DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek);

        bool UneditedRecipients(dynamic highFiveMessage);
    }
}
