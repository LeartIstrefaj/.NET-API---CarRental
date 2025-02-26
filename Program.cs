using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarRentalApp.Models
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public double PricePerDay { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string SteeringType { get; set; }
        public int NumberOfSeats { get; set; }
    }
}