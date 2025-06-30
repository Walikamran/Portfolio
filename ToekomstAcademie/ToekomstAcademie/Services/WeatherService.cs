using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class WeatherService
{
    // Simuleert het ophalen van actuele weerdata
    public async Task<WeatherData?> GetWeatherAsync(string location)
    {
        await Task.Delay(500); // Simuleer wachttijd

        if (string.IsNullOrWhiteSpace(location))
            return null;

        // Voorbeeld data voor Steenwijk
        if (location.Trim().ToLower() == "steenwijk")
        {
            return new WeatherData
            {
                Location = "Steenwijk",
                Temperature = 18,
                FeelsLike = 17,
                Condition = "Licht bewolkt",
                ConditionCode = "clouds",
                Humidity = 75,
                WindSpeed = 15,
                Sunrise = DateTime.Today.AddHours(6).AddMinutes(10),
                Sunset = DateTime.Today.AddHours(21).AddMinutes(15),
                Updated = DateTime.Now,
                News = new List<WeatherNewsArticle>
                {
                    new WeatherNewsArticle { Title = "Steenwijk geniet van lentezon", Url = "https://example.com/steenwijk-lente", Published = DateTime.Today },
                    new WeatherNewsArticle { Title = "Weersverwachting: lichte regen aanstaande vrijdag", Url = "https://example.com/steenwijk-regen", Published = DateTime.Today.AddDays(-1) }
                }
            };
        }

        // Geen data gevonden
        return null;
    }

    // Simuleer 3-daagse forecast
    public async Task<List<WeatherForecastDay>> GetForecastAsync(string location)
    {
        await Task.Delay(200);

        return new List<WeatherForecastDay>
        {
            new WeatherForecastDay { Date = DateTime.Today.AddDays(1), TempMax = 20, TempMin = 12, ConditionCode = "clear" },
            new WeatherForecastDay { Date = DateTime.Today.AddDays(2), TempMax = 19, TempMin = 11, ConditionCode = "clouds" },
            new WeatherForecastDay { Date = DateTime.Today.AddDays(3), TempMax = 16, TempMin = 9, ConditionCode = "rain" }
        };
    }
}

public class WeatherData
{
    public string Location { get; set; } = "";
    public int Temperature { get; set; }
    public int FeelsLike { get; set; }
    public string Condition { get; set; } = "";
    public string ConditionCode { get; set; } = "";
    public int Humidity { get; set; }
    public int WindSpeed { get; set; }
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
    public DateTime Updated { get; set; }
    public List<WeatherNewsArticle> News { get; set; } = new();
}

public class WeatherNewsArticle
{
    public string Title { get; set; } = "";
    public string Url { get; set; } = "";
    public DateTime Published { get; set; }
}

public class WeatherForecastDay
{
    public DateTime Date { get; set; }
    public int TempMax { get; set; }
    public int TempMin { get; set; }
    public string ConditionCode { get; set; } = "";
}
