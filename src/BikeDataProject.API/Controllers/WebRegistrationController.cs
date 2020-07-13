using System.Reflection.Metadata.Ecma335;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using BikeDataProject.API.Domain;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace BikeDataProject.API.Controllers
{
    public class WebRegistrationController : ControllerBase
    {
        private readonly BikeDataDbContext _dbContext;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly IConfiguration _configuration;

        public WebRegistrationController(BikeDataDbContext dbContext, IConfiguration configuration)
        {
            this._dbContext = dbContext;
            this._configuration = configuration;
        }

        [HttpPost("/register/strava")]
        public async Task<IActionResult> RegisterStrava(string code)
        {
            if (!String.IsNullOrWhiteSpace(code))
            {
                var data = new Dictionary<string, string>
                {
                    {"client_id", this._configuration.GetValue<String>("ClientId")},
                    {"client_secret", this._configuration.GetValue<String>("ClientSecret")},
                    {"code", code},
                    {"grant_type", "authorization_code"}
                };
                var content = new FormUrlEncodedContent(data);
                var response = await this._httpClient.PostAsync("https://www.strava.com/oauth/token", content);
                var responseString = await response.Content.ReadAsStringAsync();
                return this.Ok(responseString);
            }
            return this.BadRequest();
        }
    }
}