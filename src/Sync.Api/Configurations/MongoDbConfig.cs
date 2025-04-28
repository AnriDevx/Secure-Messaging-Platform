namespace Sync.Api.Configurations
{
    public class MongoDbConfig
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? UsersCollection { get; set; }
        public string? MessagesCollection { get; set; }
    }
}