using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AppreciationApp.Web.Services.Interfaces;
using AppreciationApp.Web.Clients.Interfaces;
using AppreciationApp.Web.Models;
using System.Linq;

namespace AppreciationApp.Web.Services
{
    public class AppreciationService : IAppreciationService
    {
        private IAppreciationAPIClient appreciationAPIClient;

        private List<string> highFiveRecieversList = new List<string>();

        private List<string> highFiveGiversList = new List<string>();

        public AppreciationService(IAppreciationAPIClient appreciationAPIClient)
        {
            this.appreciationAPIClient = appreciationAPIClient;
        }

        public List<HighFives> GetAppreciations()
        {
            var result = appreciationAPIClient.GetWeeklyHighFives();

            var highFives = new List<HighFives>();

            //Initialise list of recipients
            List<string> recipientsList = new List<string>();


            foreach (var highFive in result.results)
            {
                string highFiveMessage = highFive.text.ToString();
                string appreciator = highFive.creator_details.full_name.ToString();
                dynamic textSplit = "";
                MostHighFivesGiven(highFiveMessage, appreciator);
                var copyOfHighFive = highFiveMessage;

                while (UneditedRecipients(highFiveMessage))
                {
                    highFiveMessage = SpaceApartNames(highFiveMessage);
                }

                highFives.Add(new HighFives()
                {
                    Message = highFiveMessage,
                    AppreciatedUser = appreciator
                });
            }
            var mostRecieved = CalculateHighFiveNames(this.highFiveRecieversList);
            var mostGiven = CalculateHighFiveNames(this.highFiveGiversList);

            return highFives;
        }

        private KeyValuePair<List<string>, int> CalculateHighFiveNames(List<string> highFiveNames)
        {
            var nameGroup = highFiveNames.GroupBy(name => name);
            var maxCount = nameGroup.Max(g => g.Count());
            var names = nameGroup.Where(x => x.Count() == maxCount).Select(x => x.Key).ToList();
            return new KeyValuePair<List<string>, int>(names, maxCount);
        }

        private void MostHighFivesGiven(string highFiveMessage, string appreciator)
        {
            this.highFiveRecieversList.AddRange(highFiveMessage.Split("".ToCharArray()).Where(s => s.StartsWith("@")));
            this.highFiveGiversList.Add(appreciator);
        }

        public List<string> PopulateList(List<string> recipientsList, string recipient)
        {
            if (!string.IsNullOrEmpty(recipient))
            {
                recipientsList.Add(recipient);
            }

            return recipientsList;
        }

        public string SpaceApartNames(string message)
        {
            message = Regex.Replace(message, "[@]", "");

            return message = Regex.Replace(message, "([a-z])([A-Z])", "$1 $2");
        }

        public DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public bool UneditedRecipients(dynamic highFiveMessage)
        {
            Match match = Regex.Match(highFiveMessage, @"(@\w+)");
            if (match.Success)
            {
                return true;
            }

            return false;
        }

    }
}
