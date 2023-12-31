﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int Qty { get; set; }
    }
}
