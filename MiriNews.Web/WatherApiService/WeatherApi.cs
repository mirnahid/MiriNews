using MiriNews.Web.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiriNews.Web.WatherApiService
{
    public class WeatherApi
    {
        private readonly HttpClient _httpClient;

        public WeatherApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherApiViewModel> GetAsync()
        {
            WeatherApiViewModel model;

            var response = await _httpClient.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                model = JsonConvert.DeserializeObject<WeatherApiViewModel>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                model = null;
            }

            return model;
        }
    }
}
