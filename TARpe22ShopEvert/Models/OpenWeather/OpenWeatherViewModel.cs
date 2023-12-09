
namespace TARpe22ShopEvert.Models.OpenWeather
{
    public class OpenWeatherViewModel
    {
        public string City { get; set; }
        public int Timezone { get; set; }
        public double Temperature { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public double Speed { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}