using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using AppreciationApp.Web.Models;
using Newtonsoft.Json;

namespace AppreciationApp.Web.Repository
{
    public class FifteenFiveAppreciationRepository : IFifteenFiveAppreciationRepository
    {
        public dynamic GetWeeklyHighFives()
        {
            var APIKey = ""; //API Key Goes Here. Don't add it to the repo
            var url = @"https://theleadagency.15five.com/api/public/high-five/";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKey);

            UriBuilder builder = new UriBuilder(url);
            var dayFrom = StartOfWeek(DateTime.Today, DayOfWeek.Monday);
            builder.Query = "created_on_start=" + dayFrom.ToString("yyyy-MM-dd");

            var response = client.GetAsync(builder.Uri).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseResults = response.Content.ReadAsStringAsync().Result;
                dynamic result = JsonConvert.DeserializeObject(responseResults);
                //TODo : Make a service and move this into the service
                return result;
            }

            throw new Exception("Request to 15Five was unsuccessful");
        }

        private List<string> PopulateList(List<string> recipientsList, string recipient)
        {
            if (!string.IsNullOrEmpty(recipient))
            {
                recipientsList.Add(recipient);
            }

            return recipientsList;
        }

        private string SpaceApartNames(string message)
        {
            message = Regex.Replace(message, "[@]", "");

            return message = Regex.Replace(message, "([a-z])([A-Z])", "$1 $2");
        }

        public static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
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
