using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AmazonCrud.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        [BsonElement("code")]
        public int? code { get; set; }

        [BsonElement("name")]
        public string? name { get; set; }

        [BsonElement("main_category")]
        public string? main_category { get; set; }

        [BsonElement("sub_category")]
        public string? sub_category { get; set; }

        [BsonElement("image")]
        public string? image { get; set; }

        [BsonElement("ratings")]
        public double? ratings { get; set; }

        [BsonElement("no_of_ratings")]
        public string? no_of_ratings { get; set; }

        [BsonElement("actual_price")]
        public string? actual_price { get; set; }

        [BsonElement("discount_price")]
        public string? discount_price { get; set; }

    }
}
