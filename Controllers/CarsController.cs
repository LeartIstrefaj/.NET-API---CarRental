using CarRentalApp.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CarRentalApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly IMongoCollection<Car> _carsCollection;

        public CarsController(IMongoDatabase database)
        {
            _carsCollection = database.GetCollection<Car>("cars");
        }

        [HttpGet("rental-cars")]
        public async Task<IActionResult> GetRentalCars([FromQuery] int? year, [FromQuery] string? color, [FromQuery] string? steeringType, [FromQuery] int? numberOfSeats)
        {
            var filterBuilder = Builders<Car>.Filter;
            var filter = filterBuilder.Empty;

            if (year.HasValue)
                filter &= filterBuilder.Eq(c => c.Year, year.Value);
            if (!string.IsNullOrEmpty(color))
                filter &= filterBuilder.Eq(c => c.Color, color);
            if (!string.IsNullOrEmpty(steeringType))
                filter &= filterBuilder.Eq(c => c.SteeringType, steeringType);
            if (numberOfSeats.HasValue)
                filter &= filterBuilder.Eq(c => c.NumberOfSeats, numberOfSeats.Value);

            var cars = await _carsCollection.Find(filter).SortBy(c => c.PricePerDay).ToListAsync();
            return Ok(cars);
        }
    }
}