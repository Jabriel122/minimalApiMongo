using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace minimalAPIMongo.Domains
{
    public class Client
    {
        //Define 
        [BsonId]
        //Define o nome do compo no MongoDb como "_id" e o tipo como "ObjectId"
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id {  get; set; }

        [BsonElement("_userId"), BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("cpf")]
        public string Cpf {  get; set; }

        [BsonElement("phone")]
        public string Phone {  get; set; }

        [BsonElement("adress")]
        public string Adress { get; set; }

        [BsonElement("additionalAttributes")]
        public Dictionary<string,string> AdditionalAttributes { get; set; }

        public Client() 
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }
       

    }
}
