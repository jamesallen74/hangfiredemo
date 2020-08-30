using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace HangfireDemo.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            BackgroundJob.Enqueue(() => RunInBackground());
            Process currProcess = Process.GetCurrentProcess();
            currProcess.Id.ToString();
            Console.WriteLine($"******Get Method: PID={currProcess.Id.ToString()} Thread={Thread.CurrentThread.ManagedThreadId}");


            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

        }

        public void RunInBackground()
        {
            Process currProcess = Process.GetCurrentProcess();
            currProcess.Id.ToString();
            Console.WriteLine($"******Background Job: PID={currProcess.Id.ToString()} Thread={Thread.CurrentThread.ManagedThreadId}");
        }
    }
}