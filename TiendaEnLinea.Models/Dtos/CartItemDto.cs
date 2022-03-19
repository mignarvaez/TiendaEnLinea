using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Models.Dtos
{
    /// <summary>
    /// Clase DTO de un CartItem data object transfer, que representa una entidad con la información que sera transmitida desde la api a la pagina
    /// </summary>
    public class CartItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public string ProductName { get; set; } = string.Empty; 
        public string ProductDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }



    }
}
