using Microsoft.Data.SqlClient;
using TOHBackend.DTOS;

namespace TOHBackend.Services
{
    public class HeroAndCityServices
    {
        private readonly string _dbConnectionString;
        public HeroAndCityServices(IConfiguration config) 
        {
            string? dbConnectionString = config["ConnectionStrings:DatabaseConnection"];

            if (dbConnectionString == null) 
            {
                throw new InvalidOperationException(dbConnectionString);
            }

            _dbConnectionString = dbConnectionString;
        }

        public List<HeroAndCityDTO> GetHeroesAndCities(int Id) 
        {
            List<HeroAndCityDTO> heroesAndCities = new List<HeroAndCityDTO>();

            try
            {
                using(SqlConnection connection = new SqlConnection(_dbConnectionString)) 
                {
                    connection.Open();

                    string sql = "SELECT heroes.Id, heroes.name, cities.name FROM heroes JOIN cities ON heroes.CityId = cities.Id WHERE cities.Id = @Id";
                    using(SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("Id", Id);
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read()) 
                            {
                                HeroAndCityDTO heroAndCity = new HeroAndCityDTO();
                                heroAndCity.Id = reader.GetInt32(0);
                                heroAndCity.Name = reader.GetString(1);
                                heroAndCity.City = reader.GetString(2);

                                heroesAndCities.Add(heroAndCity);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex) 
            {
                Console.WriteLine(ex.Message);
            }

            return heroesAndCities;
        }
    }
}
