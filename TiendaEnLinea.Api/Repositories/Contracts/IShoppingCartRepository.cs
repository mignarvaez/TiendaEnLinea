using TiendaEnLinea.Api.Entities;
using TiendaEnLinea.Models.Dtos;

namespace TiendaEnLinea.Api.Repositories.Contracts
{
    /// <summary>
    /// Interfaz que representa el repositorio del carrito de compras
    /// </summary>
    public interface IShoppingCartRepository
    {   
        // Método que agregar un item
        Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto);
        
        // Método para actualizar la cantidad de un item
        Task<CartItem> UpdateQuantity(int id,CartItemQuantityUpdateDto cartItemQuantityUpdateDto);
        
        // Elimina un item
        Task<CartItem> DeleteItem(int id);

        // Obtiene un item
        Task<CartItem> GetItem(int id);

        // Retorna los item del shooping cart de un usuario
        Task<IEnumerable<CartItem>> GetItems(int userId);
    }
}
