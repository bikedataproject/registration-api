using System;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using BDPDatabase;
using BikeDataProject.Registrations.API.Configuration;
using BikeDataProject.Registrations.API.Models;
using Serilog;

namespace BikeDataProject.Registrations.API.Controllers
{
    public class WebRegistrationController : ControllerBase
    {
        private readonly BikeDataDbContext _dbContext;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly StravaApiDetails _apiDetails;

        public WebRegistrationController(BikeDataDbContext dbContext, StravaApiDetails apiDetails)
        {
            this._dbContext = dbContext;
            this._apiDetails = apiDetails;
        }

        /// <summary>
        /// Registers a new strava access token.
        /// </summary>
        /// <param name="code">The access token.</param>
        [HttpPost("/strava")]
        public async Task<IActionResult> RegisterStrava(String code)
        {
            if (String.IsNullOrWhiteSpace(code)) return this.BadRequest();
            
            var data = new StravaRegistrationRequest
            {
                ClientId = this._apiDetails.ClientId,
                ClientSecret = this._apiDetails.ClientSecret,
                Code = code
            };
            var content = new FormUrlEncodedContent(data.ToKeyValue());
            var response = await this._httpClient.PostAsync(this._apiDetails.AuthEndPoint, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var registrationObj = JsonConvert.DeserializeObject<StravaRegistrationResponse>(responseString);
            if (String.IsNullOrWhiteSpace(registrationObj.AccessToken) ||
                String.IsNullOrWhiteSpace(registrationObj.RefreshToken)) return this.BadRequest();
            
            try
            {
                var user = new User
                {
                    Provider = "web/Strava",
                    ProviderUser = registrationObj.Athlete.Id.ToString(),
                    AccessToken = registrationObj.AccessToken,
                    RefreshToken = registrationObj.RefreshToken,
                    ExpiresIn = registrationObj.ExpiresIn,
                    ExpiresAt = registrationObj.ExpiresAt
                };

                if (this._dbContext.Users.FirstOrDefault(u => u.ProviderUser == user.ProviderUser) == null)
                {
                    this._dbContext.Users.Add(user);
                    this._dbContext.SaveChanges();
                    return this.Ok(user);
                }
                return this.BadRequest("{\"message\": \"User already exists\"}");
            }
            catch (System.Exception e)
            {
                Log.Error(e, "Unhandled exception getting tokens from Strava.");
            }
            
            return this.BadRequest();
        }
    }
}