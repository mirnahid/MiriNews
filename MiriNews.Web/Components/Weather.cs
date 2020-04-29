using Microsoft.AspNetCore.Mvc;
using MiriNews.Web.Models;
using MiriNews.Web.WatherApiService;
using System.Threading.Tasks;

namespace MiriNews.Web.Components
{
    public class Weather:ViewComponent
    {
        private readonly WeatherApi _weatherApi;

        public Weather(WeatherApi weatherApi)
        {
            _weatherApi = weatherApi;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var data = await _weatherApi.GetAsync();
                return View(data);
            }
            catch (System.Exception)
            {

                return View(new WeatherApiViewModel());
            }          

          
        }
    }
}
