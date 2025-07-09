using System.ComponentModel.DataAnnotations.Schema;

namespace TOHBackend.Entities
{
    [Table("cities")]
    public class City
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string? Name { get; set; }
    }
}
