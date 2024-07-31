using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace minimalAPIMongo.Domains
{
    public class Product
    {
        //Define 
        [BsonId]
        //Define o nome do compo no MongoDb como "_id" e o tipo como "ObjectId"
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("additionalAttributes")]
        //Adiciona um dicionário para atributos adicionais
        public Dictionary<string, string> AdditionalAttributes{ get; set; }

        /// <summary>
        /// Ao ser instanciado um OBJ da classe product, o atributo AdditionalAttributes já virá com um novo dicionario 
        /// </summary>
        public Product() 
        {
            AdditionalAttributes = new Dictionary<string, string>();        
        }
    }
}
