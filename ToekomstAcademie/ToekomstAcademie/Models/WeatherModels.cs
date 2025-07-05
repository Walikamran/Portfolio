using System;
using System.Collections.Generic;

namespace ToekomstAcademie.Models
{
    public class WeatherData
    {
        public string Location { get; set; }
        public double CurrentTemp { get; set; }
        public double FeelsLike { get; set; }
        public string WeatherCondition { get; set; }
        public double WindSpeed { get; set; }
        public int Humidity { get; set; }
        public double Precipitation { get; set; }
        public int UVIndex { get; set; }
        public List<HourlyForecast> HourlyForecast { get; set; } = new List<HourlyForecast>();
        public List<DailyForecast> DailyForecast { get; set; } = new List<DailyForecast>();
    }

    public class HourlyForecast
    {
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
        public string Condition { get; set; }
    }

    public class DailyForecast
    {
        public DateTime Date { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public string Condition { get; set; }
    }
}