using Microsoft.Data.SqlClient;
using TOHBackend.Model;
using TOHBackend.DTOS;
using Azure;

namespace TOHBackend.Services
{
    public class CityService
    {
        private readonly string _dbConnectionString;  
        
        public CityService(IConfiguration configuration) 
        {
            string? dbConnectionString = configuration["ConnectionStrings:DatabaseConnection"];

            if (dbConnectionString == null) 
            {
                throw new InvalidOperationException("Missing db connection string");
            }

            _dbConnectionString = dbConnectionString;

        }
        public List<CityDTO> GetCities()
        {
            List<CityDTO> cities = new List<CityDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM cities";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CityDTO city = new CityDTO();
                                city.Id = reader.GetInt32(0);
                                city.Name = reader.GetString(1);

                                cities.Add(city);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }

            return cities;
        }

        public CityDTO GetCity(int id)
        {
            CityDTO city = new CityDTO { Id = id };

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    connection.Open();

                    string sql = $"SELECT * FROM cities WHERE Id={id}";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                city.Id = reader.GetInt32(0);
                                city.Name = reader.GetString(1);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            return city;
        }

        public CityDTO PutCity(CityDTO city)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    connection.Open();

                    string sql = "UPDATE cities SET Name = @Name WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", city.Name);
                        command.Parameters.AddWithValue("@Id", city.Id);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Console.WriteLine($"No hero found with ID {city.Id}");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }

            return city;
        }

        public CityDTO AddCity(CityDTO city)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO cities (Name) VALUES (@Name)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", city.Name);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Console.WriteLine($"No hero found with ID {city.Id}");
                        }
                    }
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }

            return city;
        }

        public string DeleteCity(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    connection.Open();

                    string sql = $"DELETE FROM cities WHERE Id = {Id} ";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }

            return $"City with the id: {Id} has been deleted";
        }
    }
}
