using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using BikeDataProject.API.Domain;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using BikeDataProject.API.Models;

namespace BikeDataProject.API.Controllers
{

    public class WebRegistrationController : ControllerBase
    {
        private readonly BikeDataDbContext _dbContext;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly String _clientId, _clientSecret, _stravaAuthEndpoint;

        public WebRegistrationController(BikeDataDbContext dbContext, IConfiguration configuration)
        {
            this._dbContext = dbContext;
            this._clientId = configuration.GetValue<String>("StravaClientId");
            this._clientSecret = configuration.GetValue<String>("StravaClientSecret");
            this._stravaAuthEndpoint = configuration.GetValue<String>("StravaAuthEndpoint");
        }

        [HttpPost("/register/strava")]
        public async Task<IActionResult> RegisterStrava(String code)
        {
            if (!String.IsNullOrWhiteSpace(code))
            {
                /*var data = new StravaRegistrationRequest
                {
                    ClientId = this._clientId,
                    ClientSecret = this._clientSecret,
                    Code = code
                };
                var content = new FormUrlEncodedContent(data.ToKeyValue());*/
                var data = new Dictionary<string, string>
                {
                    {"client_id", this._clientId},
                    {"client_secret", this._clientSecret},
                    {"code", code},
                    {"grant_type", "authorization_code"}
                };
                var content = new FormUrlEncodedContent(data);
                var response = await this._httpClient.PostAsync(this._stravaAuthEndpoint, content);
                var responseString = await response.Content.ReadAsStringAsync();
                return this.Ok(responseString);
                var registrationObj = JsonSerializer.Deserialize<StravaRegistrationResponse>(responseString);
                if (!String.IsNullOrWhiteSpace(registrationObj.AccessToken) && !String.IsNullOrWhiteSpace(registrationObj.RefreshToken) && registrationObj.ExpiresAt != 0)
                {
                    return this.Ok(registrationObj);
                }
            }
            return this.BadRequest();
        }
    }
}