using MongoDB.Driver;

namespace minimalAPIMongo.Services
{
    public class MongoDbService
    {

        /// <summary>
        /// Armazenar configuração da aplicação
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Armazenar uma referência ao MongoDB
        /// </summary>
        private readonly IMongoDatabase _database;

        /// <summary>
        /// Recebe a config da aplicações como parâmetro
        /// </summary>
        /// <param name="configuration"></param>
        public MongoDbService(IConfiguration configuration)
        {
            //Atribui a config recebida em _configuiration
            _configuration = configuration;
            //Obtem a string de conexão atraves do _configuration
            var connectionString = _configuration.GetConnectionString("DbConnetion");

            //Criar um obj MongoUrl que recebe como parâmetro a string de conexão
            var mongoUrl = MongoUrl.Create(connectionString);

            //Cria um client MongoClient para se conectar ao MongoDb
            var mongoClinent = new MongoClient(mongoUrl);

            //Obtem a rederÇencia ao banco com o nome especificado na string de conexçao
            _database = mongoClinent.GetDatabase(mongoUrl.DatabaseName);

        }

        /// <summary>
        /// Propriedade para acessar o banco de dados
        /// Retorna a referÇencia ai Banco de 
        /// </summary>
        public IMongoDatabase GetDatabase => _database;
    }
}
