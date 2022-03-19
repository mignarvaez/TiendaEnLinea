﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Models.Dtos
{   
    /// <summary>
    /// Clase DTO que representa un producto
    /// </summary>
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } =  string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;   
        public decimal Price { get; set; }  
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
