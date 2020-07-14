using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BikeDataProject.API.Domain;
using BikeDataProject.API.Models;
using Microsoft.Extensions.Configuration;


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
                var data = new StravaRegistrationRequest
                {
                    ClientId = this._clientId,
                    ClientSecret = this._clientSecret,
                    Code = code
                };
                var content = new FormUrlEncodedContent(data.ToKeyValue());
                var response = await this._httpClient.PostAsync(this._stravaAuthEndpoint, content);
                var responseString = await response.Content.ReadAsStringAsync();
                var registrationObj = JsonSerializer.Deserialize<StravaRegistrationResponse>(responseString);
                if (!String.IsNullOrWhiteSpace(registrationObj.AccessToken) && !String.IsNullOrWhiteSpace(registrationObj.RefreshToken))
                {
                    try
                    {
                        var user = new User
                        {
                            Provider = "web/Strava",
                            ProviderId = registrationObj.Athlete.Id,
                            AccessToken = registrationObj.AccessToken,
                            RefreshToken = registrationObj.RefreshToken,
                            ExpiresIn = registrationObj.ExpiresIn,
                            ExpiresAt = registrationObj.ExpiresAt
                        };
                        return this.Ok(user);
                        this._dbContext.Users.Add(user);
                        return this.Ok(registrationObj);
                    }
                    catch (System.Exception)
                    {
                        return this.BadRequest();
                    }
                }
            }
            return this.BadRequest();
        }
    }
}