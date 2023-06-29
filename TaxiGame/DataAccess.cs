using MySql.Data.MySqlClient;

namespace TaxiGame
{
    public class DataAccess
    {
        public static string ConnectionString = "Server=localhost;Port=3306;Database=taxi_game;Uid=root;password=root;";
        public static MySqlConnection MySqlConnection = new MySqlConnection(ConnectionString);
    }
}
