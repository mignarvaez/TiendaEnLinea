using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Models.Dtos
{
    /// <summary>
    /// Clase DTO que representa la actualización de cantidad de un cart item
    /// </summary>
    public class CartItemQuantityUpdateDto
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
