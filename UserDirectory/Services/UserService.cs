using UserDirectory.Models;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

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

        public List<Users> GetUsersByFilter(string columnName, string filterValue)
        {
            var users = new List<Users>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE {columnName} = @FilterValue";

                using(var command = new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@FilterValue", filterValue);
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

        // SQL Insert method
        public bool AddUser(Users users)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (FirstName, PostalAddress, City, Country, ZipCode) " + 
                    "VALUES (@FirstName, @PostalAddress, @City, @Country, @ZipCode)";

                using (var command =  new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(@"FirstName", users.FirstName);
                    command.Parameters.AddWithValue(@"PostalAddress", users.PostalAddress);
                    command.Parameters.AddWithValue(@"City", users.City);
                    command.Parameters.AddWithValue(@"Country", users.Country) ;
                    command.Parameters.AddWithValue(@"FirstName", users.ZipCode);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        //SQL Update method

        public bool UpdateUser(Users users) 
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Users SET FirstName = @FirstName, PostalAddress = @PostalAddress, " +
                    "City = @City, Country = @Country, ZipCode = @ZipCode WHERE UserID = @UserID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(@"FirstName", users.FirstName);
                    command.Parameters.AddWithValue(@"PostalAddress", users.PostalAddress);
                    command.Parameters.AddWithValue(@"City", users.City);
                    command.Parameters.AddWithValue(@"Country", users.Country);
                    command.Parameters.AddWithValue(@"FirstName", users.ZipCode);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool DeleteUser(Users users)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                string query = "DELETE FROM Users WHERE UserID = @UserID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", users.UserID);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;  // Returns true if deletion was successful
                }
            }
            



    }
}
