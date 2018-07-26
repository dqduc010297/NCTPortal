using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using GoGoSpa.Models;
using Microsoft.Extensions.Options;

namespace GoGoSpa.Controllers
{
    [Route("configuration")]
    public class ConfigurationController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly IOptionsSnapshot<AppSettings> _settings;

        public ConfigurationController(IHostingEnvironment env, IOptionsSnapshot<AppSettings> settings)
        {
            _env = env;
            _settings = settings;
        }


        [HttpGet]
        [Route("")]
        public IActionResult GetConfiguration()
        {
            return Ok(GetConfigurationData());
        }

        [HttpGet]
        [Route("serviceRegistry")]
        public IActionResult GetServiceRegistry()
        {
            return Ok(_settings.Value.ServiceRegistry);
        }

        [HttpGet]
        [Route("js")]
        public IActionResult GetConfigurationScript()
        {
            var model = GetConfigurationData();
            return View(model);
        }

        private ConfigurationDto GetConfigurationData()
        {
            return new ConfigurationDto
            {
                EnvironmentName = _env.EnvironmentName,
                AppSettings = _settings.Value
            };
        }
    }
}