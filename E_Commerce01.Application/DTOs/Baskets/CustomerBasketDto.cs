using E_Commerce01.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.DTOs.Baskets
{
    public class CustomerBasketDto
    {
        [Required(ErrorMessage = "Basket id is required")]
        public string Id { get; set; } // Guid
        public ICollection<BasketItemDto> Items { get; set; } = [];
    }
}
