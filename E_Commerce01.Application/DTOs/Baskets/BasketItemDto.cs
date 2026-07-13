using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.DTOs.Baskets
{
    public class BasketItemDto
    {
        [Required(ErrorMessage = "Product id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;

        [Range(1 , double.MaxValue ,ErrorMessage ="Price must be a positive number" )]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }
}
