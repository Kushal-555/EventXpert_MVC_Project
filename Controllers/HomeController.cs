using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventXpert.Data;
using EventXpert.Models;               // <-- HomeViewModel, CurrentWeather, HourlyWeather, LiveMatch, etc.
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EventXpert.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public HomeController(
            ApplicationDbContext context,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel();

            /* -------------------------------------------------
               1. Upcoming Events
               ------------------------------------------------- */
            model.UpcomingEvents = await _context.Events
                .OrderBy(e => e.Date)
                .Take(5)
                .ToListAsync();

           // 2. Tampa Weather + Precipitation %
            try
            {
                var weatherUrl = "https://api.open-meteo.com/v1/forecast" +
                                "?latitude=27.9506" +
                                "&longitude=-82.4572" +
                                "&current_weather=true" +
                                "&hourly=precipitation_probability" +
                                "&timezone=America/New_York";

                var weatherJson = await _httpClient.GetStringAsync(weatherUrl);

                // Optional: Save for debug
                // System.IO.File.WriteAllText("wwwroot/debug_weather.json", weatherJson);

                var weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(
                    weatherJson, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (weatherResponse?.CurrentWeather != null)
                {
                    model.TampaWeather = weatherResponse.CurrentWeather;

                    // Match current hour
                    if (weatherResponse.Hourly?.Time != null && 
                        weatherResponse.Hourly.PrecipitationProbability != null)
                    {
                        var now = DateTime.Now;
                        var currentHour = now.ToString("yyyy-MM-ddTHH:00");
                        int index = weatherResponse.Hourly.Time.FindIndex(t => t == currentHour);
                        if (index >= 0)
                        {
                            model.TampaWeather.PrecipitationProbability = weatherResponse.Hourly.PrecipitationProbability[index];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Optional: Log to console for Azure
                Console.WriteLine($"Weather API Error: {ex.Message}");
                model.TampaWeather = new CurrentWeather
                {
                    Temperature = 0,
                    Windspeed = 0,
                    Weathercode = -1,
                    PrecipitationProbability = 0
                };
            }
            /* -------------------------------------------------
               3. Live Soccer Scores (TheSportsDB v1)
               ------------------------------------------------- */
            try
            {
                var apiKey = _configuration["TheSportsDbApiKey"] ?? "123";
                var liveScoreUrl = $"https://www.thesportsdb.com/api/v1/json/{apiKey}/livescore.php";

                var scoreJson = await _httpClient.GetStringAsync(liveScoreUrl);
                var scoreResponse = JsonSerializer.Deserialize<LiveScoreResponse>(
                    scoreJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                model.LiveMatches = scoreResponse?.Events?
                    .Where(m => !string.IsNullOrEmpty(m.StrHomeTeam) && !string.IsNullOrEmpty(m.StrAwayTeam))
                    .Take(8)
                    .ToList() ?? new List<LiveMatch>();
            }
            catch
            {
                model.LiveMatches = new List<LiveMatch>();
            }

            return View(model);
        }

        /* -------------------------------------------------
           Other actions (Data, About) – unchanged
           ------------------------------------------------- */
        public async Task<IActionResult> Data()
        {
            var allEvents = await _context.Events
                .OrderBy(e => e.Date)
                .ToListAsync();
            return View(allEvents);
        }

        public IActionResult About() => View();
    }

    /* -----------------------------------------------------------------
       Helper DTOs – **these are now defined in Models/HomeViewModel.cs**
       (kept here only as a fallback if you ever delete the file)
       ----------------------------------------------------------------- */
    // If you keep the classes here, comment them out – they would duplicate
    // the ones in HomeViewModel.cs and cause a compile error.
    /*
    public class WeatherResponse { ... }
    public class CurrentWeather { ... }
    public class HourlyWeather { ... }
    public class LiveScoreResponse { ... }
    */
}