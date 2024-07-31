using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace minimalAPIMongo.Domains
{
    public class User
    {
        //Define 
        [BsonId]
        //Define o nome do compo no MongoDb como "_id" e o tipo como "ObjectId"
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string name {  get; set; }

        [BsonElement("email")]
        public string email { get; set; }

        [BsonElement("password")]
        public string password { get; set; }

        [BsonElement("additionalAttributes")]
        public Dictionary<string, string> AdditionalAttributes { get; set; }

        public User() 
        { 
            AdditionalAttributes = new Dictionary<string, string>();
        }

    } 
}
