using CarRentalApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CarRentalApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IMongoCollection<User> _usersCollection;

        public ProfileController(IMongoDatabase database)
        {
            _usersCollection = database.GetCollection<User>("users");
        }

        [HttpGet("my-profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var username = User.Identity.Name;
            var user = await _usersCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user == null)
                return NotFound();

            return Ok(new { user.FullName, user.Username, user.Email });
        }
    }
}