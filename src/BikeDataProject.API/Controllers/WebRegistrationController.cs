using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using BikeDataProject.API.Domain;
using System.Net.Http;

namespace BikeDataProject.API.Controllers
{
    public class WebRegistrationController : ControllerBase
    {
        private readonly BikeDataDbContext _dbContext;
        private readonly HttpClient _httpClient = new HttpClient();

        public WebRegistrationController(BikeDataDbContext dbContext) => this._dbContext = dbContext;

        [HttpPost("/register/strava")]
        public async Task<IActionResult> RegisterStrava(string code)
        {
            if (!String.IsNullOrWhiteSpace(code))
            {
                var values = new Dictionary<string, string>
                {
                    {"client_id", "CLIENT_ID"},
                    {"client_secret", "CLIENT_SECRET"},
                    {"code", code},
                    {"grant_type", "authorization_code"}
                };

                var content = new FormUrlEncodedContent(values);

                var response = await this._httpClient.PostAsync("https://www.strava.com/oauth/token", content);

                var responseString = await response.Content.ReadAsStringAsync();
                return this.Ok(String.Format("response is {0}", responseString));
            }
            return this.BadRequest();
        }
    }
}