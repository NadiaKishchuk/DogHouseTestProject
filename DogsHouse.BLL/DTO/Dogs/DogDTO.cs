using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DogsHouse.BLL.DTO.Dogs
{
    public class DogDTO
    {
        [Required, MaxLength(30), MinLength(2)]
        public string Name { get; set; }

        [Required, MaxLength(40), MinLength(2)]
        public string Color { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int tail_length { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Weight { get; set; }
    }
}
