using EventXpert.Data;
using System.Text.Json.Serialization;

namespace EventXpert.Models
{
    public class WeatherResponse
    {
        [JsonPropertyName("current_weather")]
        public CurrentWeather? CurrentWeather { get; set; }

        [JsonPropertyName("hourly")]
        public HourlyWeather? Hourly { get; set; }
    }
    public class HourlyWeather
    {
        [JsonPropertyName("time")]
        public List<string>? Time { get; set; }

        [JsonPropertyName("precipitation_probability")]
        public List<int>? PrecipitationProbability { get; set; }
    }
    public class CurrentWeather
    {
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("windspeed")]
        public double Windspeed { get; set; }

        [JsonPropertyName("weathercode")]
        public int Weathercode { get; set; }

        public int PrecipitationProbability { get; set; } = 0;
    }
    public class HomeViewModel
    {
        public List<Event> UpcomingEvents { get; set; } = new();
        public CurrentWeather? TampaWeather { get; set; }
        public List<LiveMatch> LiveMatches { get; set; } = new();
    }

    /* ------------------ Sports ------------------ */
    public class LiveMatch
    {
        public string? StrEvent { get; set; }
        public string? StrHomeTeam { get; set; }
        public string? StrAwayTeam { get; set; }
        public string? IntHomeScore { get; set; }
        public string? IntAwayScore { get; set; }
        public string? StrProgress { get; set; }
        public string? DateEvent { get; set; }
        public string? StrLeague { get; set; }
        public string? StrThumb { get; set; }
    }

    public class LiveScoreResponse
    {
        public List<LiveMatch>? Events { get; set; }
    }
}