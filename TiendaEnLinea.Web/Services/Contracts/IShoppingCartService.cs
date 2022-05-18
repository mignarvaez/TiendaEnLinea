using TiendaEnLinea.Models.Dtos;

namespace TiendaEnLinea.Web.Services.Contracts
{
    /// <summary>
    /// Interfaz que representa el servicio para interactuar con el carrito de compras
    /// </summary>
    public interface IShoppingCartService
    {
        // Método para obtener un articulo
        Task<List<CartItemDto>> GetItems(int userId);

        // Método para agregar un articulo
        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);

        // Método para eliminar un articulo
        Task<CartItemDto> DeleteItem(int id);
    }
}
