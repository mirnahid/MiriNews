using MiriNews.Web.Models.Weather;

namespace MiriNews.Web.Models
{
    public class WeatherApiViewModel
    {
        public Location location{ get; set; }

        public Current current { get; set; }

    }
}
