using System.Collections.Generic;
using System.Diagnostics;
using AppreciationApp.Web.Models;
using AppreciationApp.Web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppreciationApp.Web.Controllers
{
    public class AppreciationController : Controller
    {
        private readonly IFifteenFiveAppreciationRepository repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppreciationController"/> class.
        /// </summary>
        public AppreciationController(IFifteenFiveAppreciationRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
          
            var highfives = repo.GetWeeklyHighFives();
            var AppreciationViewModel = new List<AppreciationViewModel>();
            //int indexCount = 0;
            //foreach (var item in highfives)
            //{
            //    indexCount += 1;
            //    AppreciationViewModel.Add(new AppreciationViewModel()
            //    {
            //        Index = indexCount,
            //        Message = item.Message,
            //        Username = item.AppreciatedUser
            //    });
            //}
       

            return View(AppreciationViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
