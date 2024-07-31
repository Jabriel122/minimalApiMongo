using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace minimalAPIMongo.Domains
{
    public class Order
    {
        //Define 
        [BsonId]
        //Define o nome do compo no MongoDb como "_id" e o tipo como "ObjectId"
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("stats")]
        public string Status { get; set; }



        //referência aos produto do pedido

        //- Referência para que eu consiga cadastrar um pedido com os produto
        [BsonElement("productId")]
        [JsonIgnore]//Evitar que quando "listarmos" venha os ids dos produtos
        public List<string> ProductId { get; set; }

        //- Referência para que quando eu list eos pedidos, venham os dados de cada produto(lista)
    
        public List<Product> Product { get; set; }

        //Referência ao cliente que está fazendo o pedido

        //- Refência para que eu consiga cadastrar um pedido do cliente

        [BsonElement("clientId")]
        [JsonIgnore]//Evitar que quando "listarmos" venha os ids dos cliente
        public string clientId { get; set; }

        //- Refência para que quando eu liste os pedidos, venham os dados do cliente
        
        public Client Client { get; set; }

    }
}
