using System;
using System.Collections.Generic;
using System.Linq;
using DahlexApp.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DahlexApp.Logic.Models;

namespace DahlexApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        private readonly ILogger<TestController> _logger;
        private readonly IScoringService _scoring;

        public TestController(ILogger<TestController> logger, IScoringService scoring)
        {
            _logger = logger;
            _scoring = scoring;
        }

        [HttpGet]
        public IEnumerable<TestThing> Get()
        {
            _logger.LogError("Testar");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new TestThing()
            {
                Id = index,
                Text = $"text {index}",
                Description = $"description {index}"
            })
            .ToArray();
        }
    }
}
