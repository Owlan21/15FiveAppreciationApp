using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppreciationApp.Web.Models;
using AppreciationApp.Web.Services.Interfaces;
using AppreciationApp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppreciationApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppreciationController : ControllerBase
    {
        private readonly IAppreciationService appreciationService;
        public AppreciationController(IAppreciationService appreciationService)
        {
            this.appreciationService = appreciationService;
        }
        public AppreciationViewModel Get15FiveAppreciations([FromQuery]string postcode)
        {
            var fifteenFive = appreciationService.GetAppreciations();
            var appreciationViewModel = new AppreciationViewModel();
            int indexCount = 0;
            foreach (var item in fifteenFive.HighFives)
            {
                indexCount += 1;
                appreciationViewModel.HighFives.Add(new HighFivesViewModel()
                {
                    Index = indexCount,
                    Message = item.Message,
                    Username = item.AppreciatedUser
                });
            }
            appreciationViewModel.MostGiven = fifteenFive.MostGiven;
            appreciationViewModel.MostRecieved = fifteenFive.MostRecieved;
            return appreciationViewModel;
        }
    }
}