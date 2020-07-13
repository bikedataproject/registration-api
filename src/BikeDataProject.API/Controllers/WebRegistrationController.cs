using Microsoft.AspNetCore.Mvc;
using BikeDataProject.API.Domain;

namespace BikeDataProject.API.Controllers
{
    public class WebRegistrationController : ControllerBase
    {
        private readonly BikeDataDbContext _dbContext;

        public WebRegistrationController(BikeDataDbContext dbContext) => this._dbContext = dbContext;

        [HttpPost("/register/strava")]
        public IActionResult RegisterStrava()
        {
            return this.Ok("Hello, world!");
        }
    }
}