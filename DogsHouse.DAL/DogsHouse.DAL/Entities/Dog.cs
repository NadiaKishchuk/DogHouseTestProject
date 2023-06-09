using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogsHouse.DAL.Entities
{
    public class Dog
    {
        [Required, Key, MaxLength(30), Column("name")]
        public string? Name { get; set; }

        [Required, MaxLength(40),Column("color")]
        public string? Color { get; set; }

        [Required, Range(0, int.MaxValue), Column("tail_length")]
        public int TailLength { get; set; }

        [Required, Range(0, int.MaxValue), Column("weight")]
        public int Weight { get; set; }
    }
}
