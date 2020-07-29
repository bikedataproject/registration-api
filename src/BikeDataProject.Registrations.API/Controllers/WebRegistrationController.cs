using System;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BDPDatabase;
using BikeDataProject.Registrations.API.Configuration;
using BikeDataProject.Registrations.API.Models;
using Serilog;
using BikeDataProject.Registrations.API.Helpers;

namespace BikeDataProject.Registrations.API.Controllers
{
    /// <summary>
    /// Controller that handles all the registrations coming from the web- and mobile-application
    /// </summary>
    public class WebRegistrationController : ControllerBase
    {
        private readonly BikeDataDbContext _dbContext;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly StravaApiDetails _apiDetails;

        /// <summary>
        /// Initialises a new instance of the WebRegistrationController.
        /// Sets the Database context as dbContext and loads the details of the Strava API.
        /// </summary>
        /// <param name="dbContext">dbContext</param>
        /// <param name="apiDetails">apiDetails</param>
        public WebRegistrationController(BikeDataDbContext dbContext, StravaApiDetails apiDetails)
        {
            this._dbContext = dbContext;
            this._apiDetails = apiDetails;
        }

        /// <summary>
        /// Requests the user info from Strava given the token exchange code .
        /// Creates a new strava user in the database and sets the Provideruser, the acces token and refresh token.
        /// </summary>
        /// <param name="code">The token exchange code</param>
        [HttpGet("/strava")]
        public async Task<IActionResult> RegisterStrava(String code)
        {
            if (String.IsNullOrWhiteSpace(code))
            {
                Log.Error("Code is null");
                return this.BadRequest();
            }
            
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
                String.IsNullOrWhiteSpace(registrationObj.RefreshToken))
                {
                    Log.Error($"Access token or refresh token is null, response: {responseString}");
                    return this.BadRequest();
                }
            
            try
            {
                var user = new User
                {
                    Provider = "web/Strava",
                    UserIdentifier = Guid.NewGuid(),
                    ProviderUser = registrationObj.Athlete.Id.ToString(),
                    AccessToken = registrationObj.AccessToken,
                    RefreshToken = registrationObj.RefreshToken,
                    ExpiresIn = registrationObj.ExpiresIn,
                    ExpiresAt = registrationObj.ExpiresAt,
                    TokenCreationDate = DateTime.UtcNow,
                    IsHistoryFetched = false
                };

                if (this._dbContext.Users.FirstOrDefault(u => u.ProviderUser == user.ProviderUser) == null)
                {
                    Log.Information($"New user is going to be added ProviderUser: {user.ProviderUser}");
                    this._dbContext.Users.Add(user);
                    this._dbContext.SaveChanges();
                }
                else
                {
                    Log.Information($"User already exists, ProviderUser: {user.ProviderUser}");
                }

                Log.Information("Everything is good, redirection");
                return this.Redirect(this._apiDetails.RedirectionUri);
            }
            catch (System.Exception e)
            {
                Log.Error(e, "Unhandled exception getting tokens from Strava.");
            }
            
            return this.BadRequest("{\"message:\": \"Unable to register the user\"}");
        }
        
        /// <summary>
        /// Method to retrieve an existing user or create a new one from the mobile app.
        /// </summary>
        /// <param name="userInfo">The info of the user provided by the mobile app</param>
        /// <returns>UserInfo of the registered user + Ok or Created</returns>
        [HttpPost("/MobileApp")]
        public IActionResult RegisterMobileApp([FromBody]UserInfo userInfo)
        {
            if(string.IsNullOrEmpty(userInfo.Imei))
            {
                return this.BadRequest();
            }
            var hashedIMEI = Hasher.GetHashString(userInfo.Imei);
            var user = this._dbContext.ContainsProviderUser(hashedIMEI);
            if( user != null)
            {
                userInfo.UserIdentifier = user.UserIdentifier;
                return this.Ok(userInfo);
            }
            user = new User()
            {
                UserIdentifier = Guid.NewGuid(),
                Provider = "mobileapp",
                ProviderUser = hashedIMEI,
                AccessToken = string.Empty,
                RefreshToken = string.Empty,
                TokenCreationDate = DateTime.MinValue,
                ExpiresAt = -1,
                ExpiresIn = -1
            };
            this._dbContext.AddUser(user);
            this._dbContext.SaveChanges();
            userInfo.UserIdentifier = user.UserIdentifier;
            return this.Created(this.Request.Path, userInfo);
        }
    }
}