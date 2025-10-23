

using Microsoft.Data.SqlClient;
using TOHBackend.Model;

namespace TOHBackend.Services
{
    public class HeroService
    {
        private readonly string _dbConnectionString;

        public HeroService(IConfiguration configuration)
        {
            string? dbConnectionString = configuration["ConnectionStrings:DatabaseConnection"];

            if (dbConnectionString == null)
            {
                throw new InvalidOperationException("Missing db connection string");
            }

            _dbConnectionString = dbConnectionString;

        }
        //Look into EF Core {easier way}
        public List<HeroDTO> GetHeroes()
        {
            List<HeroDTO> heroes = [];

            try
            {
                using SqlConnection connection = new(_dbConnectionString);
                connection.Open();

                string sql = "SELECT * FROM heroes";
                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && !reader.IsDBNull(1) && !reader.IsDBNull(2))
                    {
                        HeroDTO hero = new()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            CityId = reader.GetInt32(2)
                        };

                        heroes.Add(hero);
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }

            return heroes;
        }

        public HeroDTO GetHero(int id)
        {
            HeroDTO hero = new HeroDTO { Id = id };

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    connection.Open();

                    string sql = $"SELECT * FROM heroes WHERE Id={id}";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                hero.Id = reader.GetInt32(0);
                                hero.Name = reader.GetString(1);
                                hero.CityId = reader.GetInt32(2);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            return hero;
        }

        public HeroDTO PutHero(HeroDTO hero)
        {
            try
            {
                if (hero.CityId.HasValue)
                {
                    using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                    {
                        connection.Open();

                        string sql = "UPDATE heroes SET Name = @Name, CityId = @CityId WHERE Id = @Id";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Name", hero.Name);
                            command.Parameters.AddWithValue("@CityId", hero.CityId);
                            command.Parameters.AddWithValue("@Id", hero.Id);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                Console.WriteLine($"No hero found with ID {hero.Id}");
                            }
                        }
                    }
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                    {
                        connection.Open();

                        string sql = "UPDATE heroes SET Name = @Name, CityId = @CityId WHERE Id = @Id";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Name", hero.Name);
                            command.Parameters.AddWithValue("@CityId", DBNull.Value);
                            command.Parameters.AddWithValue("@Id", hero.Id);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                Console.WriteLine($"No hero found with ID {hero.Id}");
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }

            return hero;
        }

        public HeroDTO AddHero(HeroDTO hero)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO heroes (Name, CityId) VALUES (@Name, @CityId)";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@Name", hero.Name);
                        command.Parameters.AddWithValue("@CityId", hero.CityId);
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            //You are checking like a dummy
                            //Entity Framework will solve this problem with not 
                            Console.WriteLine($"No hero found with ID {hero.Id}");
                        }
                    }
                }
            }

            catch(SqlException ex)
            {
                Console.WriteLine(ex);
            }

            return hero;
        }

        public string DeleteHero(int Id)
        {
            try
            {
               using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    connection.Open();

                    string sql = $"DELETE FROM heroes WHERE Id = {Id} ";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex);
            }

            return $"Hero with the id: {Id} has been deleted";
        }
    }
}
