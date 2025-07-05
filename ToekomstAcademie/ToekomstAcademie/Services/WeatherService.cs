using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ToekomstAcademie.Models;

namespace ToekomstAcademie.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "0d1f9dc125bb1fccb4379234a4b132f3"; // Vervang dit met je echte API key
        private const string BaseUrl = "https://home.openweathermap.org/api_keys";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherData> GetWeatherDataAsync(string location)
        {
            try
            {
                // Eerst coordinaten ophalen
                var geoResponse = await _httpClient.GetFromJsonAsync<GeoResponse>(
                    $"{BaseUrl}weather?q={location}&appid={ApiKey}&units=metric&lang=nl");

                if (geoResponse == null)
                    throw new Exception("Locatie niet gevonden");

                // Dan weerdata ophalen
                var response = await _httpClient.GetFromJsonAsync<WeatherApiResponse>(
                    $"{BaseUrl}onecall?lat={geoResponse.Coord.Lat}&lon={geoResponse.Coord.Lon}&exclude=minutely&appid={ApiKey}&units=metric&lang=nl");

                if (response == null)
                    throw new Exception("Weerdata niet gevonden");

                return MapToWeatherData(response, geoResponse.Name);
            }
            catch (Exception ex)
            {
                throw new Exception($"Fout bij ophalen weerdata: {ex.Message}");
            }
        }

        private WeatherData MapToWeatherData(WeatherApiResponse apiResponse, string location)
        {
            return new WeatherData
            {
                Location = location,
                CurrentTemp = apiResponse.Current.Temp,
                FeelsLike = apiResponse.Current.FeelsLike,
                WeatherCondition = apiResponse.Current.Weather[0].Description,
                WindSpeed = apiResponse.Current.WindSpeed * 3.6, // m/s naar km/u
                Humidity = apiResponse.Current.Humidity,
                Precipitation = apiResponse.Current.Rain?.OneHour ?? 0,
                UVIndex = (int)apiResponse.Current.Uvi,
                HourlyForecast = apiResponse.Hourly.Select(h => new HourlyForecast
                {
                    Time = DateTimeOffset.FromUnixTimeSeconds(h.Dt).DateTime,
                    Temperature = h.Temp,
                    Condition = h.Weather[0].Description
                }).Take(24).ToList(),
                DailyForecast = apiResponse.Daily.Select(d => new DailyForecast
                {
                    Date = DateTimeOffset.FromUnixTimeSeconds(d.Dt).DateTime,
                    MaxTemp = d.Temp.Max,
                    MinTemp = d.Temp.Min,
                    Condition = d.Weather[0].Description
                }).Take(5).ToList()
            };
        }
    }

    // Helper classes voor API response
    public class WeatherApiResponse
    {
        public CurrentWeather Current { get; set; }
        public List<HourlyWeather> Hourly { get; set; }
        public List<DailyWeather> Daily { get; set; }
    }

    public class CurrentWeather
    {
        public long Dt { get; set; }
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double Uvi { get; set; }
        public List<WeatherDescription> Weather { get; set; }
        public Rain Rain { get; set; }
    }

    public class HourlyWeather
    {
        public long Dt { get; set; }
        public double Temp { get; set; }
        public List<WeatherDescription> Weather { get; set; }
    }

    public class DailyWeather
    {
        public long Dt { get; set; }
        public DailyTemp Temp { get; set; }
        public List<WeatherDescription> Weather { get; set; }
    }

    public class DailyTemp
    {
        public double Max { get; set; }
        public double Min { get; set; }
    }

    public class WeatherDescription
    {
        public string Description { get; set; }
    }

    public class Rain
    {
        public double OneHour { get; set; }
    }

    public class GeoResponse
    {
        public string Name { get; set; }
        public Coord Coord { get; set; }
    }

    public class Coord
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
