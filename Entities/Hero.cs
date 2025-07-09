using System.ComponentModel.DataAnnotations.Schema;

namespace TOHBackend.Entities
{
    [Table("heroes")]
    public class Hero
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string? Name { get; set; }

        [Column("CityId")]
        public int? CityId { get; set; }
    }
}
