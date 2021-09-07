namespace UserAuthIdentityApi.Settings
{
    public class PostgresqlSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Userid { get; set; } //We are using authentication with mongodb now. Theese properties must added to connection string.
        public string Password { get; set; } //Password setted with .Net secret manager in terminal. This prevents security issues like leaking passwords.
        public string ConnectionString //Calculate connectionString that's needed in order to talk to MongoDB.
        {
            get
            {
                string URI = $"Server={Server};Port={Port};Database={Database};User Id={Userid};Password={Password}"; //This is syntax type that MongoDb expecting from us.
                return URI;
            }
        }
    }
}