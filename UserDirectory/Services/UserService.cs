using UserDirectory.Models;
using System.Data.SqlClient;

namespace UserDirectory.Services
{
    public class UserService
    {
        private readonly string? _connectionString;

        
        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Users> GetListOfUsers()
        {
            var users = new List<Users>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Users", connection) )
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new Users
                            {
                                UserID = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                PostalAddress = reader.GetString(2),
                                City = reader.GetString(3),
                                Country = reader.GetString(4),
                                ZipCode = reader.GetString(5),
                            });

                        }
                    }
                }
            }
            return users;
        }
    }
}
