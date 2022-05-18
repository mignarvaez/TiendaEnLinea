using Microsoft.AspNetCore.Components;
using TiendaEnLinea.Models.Dtos;
using TiendaEnLinea.Web.Services.Contracts;

namespace TiendaEnLinea.Web.Pages
{   
    /// <summary>
    /// Clase basea para el componente de blazor asociado al carrito de compras
    /// </summary>
    public class ShoppingCartBase:ComponentBase
    {
        // El servicio del carrito de compras
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        // Lita de items
        public List<CartItemDto> ShoppingCartItems { get; set; }

        // Mensaje de error
        public string ErrorMessage { get; set; }   

        /// <summary>
        /// Método que carga los items del carrito de compra al inicializar el componente
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        /// <summary>
        /// Evento asociado al click que permite eliminar un item del carrito de compras
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected async Task DeleteCartItem_Click(int id)
        {   
            // Hace el llamado al api para eliminar el producto
            // y remueve el mismo en el lado del cliente
            var cartItemDto = await ShoppingCartService.DeleteItem(id);
            RemoveCartItem(id);
        }

        /// <summary>
        /// Método que permite eliminar el item del carrito de compras desde el lado del cliente sin tener que
        /// hacer el llamado al backend para obtener los items actuales
        /// </summary>
        /// <param name="id"></param>
        private void RemoveCartItem(int id)
        {   
            // Busca el item a borrar
            var carItemDto = GetCartItem(id);
            ShoppingCartItems.Remove(carItemDto);
        }

        /// <summary>
        ///  Método encargado de obtener un item del carrito de compras del lado del cliente según id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private CartItemDto GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
        }
    }
}
