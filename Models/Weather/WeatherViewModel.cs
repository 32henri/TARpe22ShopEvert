using TARpe22ShopEvert.Core.Dto.WeatherDtos;

namespace TARpe22ShopEvert.Models.Weather
{
    public class WeatherViewModel
    {
        public DateTime Date { get; set; }
        public int EpochDate { get; set; }
        public int Severity { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
        public double TempMinValue { get; set; }
        public string TempMinUnit { get; set; }
        public int TempMinUnitType { get; set; }
        public double TempMaxValue { get; set; }
        public string TempMaxUnit { get; set; }
        public int TempMaxUnitType { get; set; }
        public int DayIcon { get; set; }
        public string DayIconPhrase { get; set; }
        public bool DayHasPrecipitation { get; set; }
        public string DayPrecipitationType { get; set; }
        public string DayPrecipitationIntensity { get; set; }
        public int NightIcon { get; set; }
        public string NightIconPhrase { get; set; }
        public bool NightHasPrecipitation { get; set; }
        public string NightPrecipitationType { get; set; }
        public string NightPrecipitationIntensity { get; set; }
    }

    public class Temperatures
    {
        public Temperature Minimum { get; set; }
        public Temperature Maximum { get; set; }
    }

    public class DayNightCycles
    {
        public int Icon { get; set; }
        public string IconPhrase { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public string PrecipitationIntensity { get; set; }
    }
    public class Temperature
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }
}