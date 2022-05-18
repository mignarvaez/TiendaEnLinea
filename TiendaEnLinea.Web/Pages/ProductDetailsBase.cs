using Microsoft.AspNetCore.Components;
using TiendaEnLinea.Models.Dtos;
using TiendaEnLinea.Web.Services.Contracts;

namespace TiendaEnLinea.Web.Pages
{
    /// <summary>
    /// Clase que sirve para el componente de de talle de producto
    /// </summary>
    public class ProductDetailsBase:ComponentBase
    {
        // Id declarada como parametro
        [Parameter]
        public int Id { get; set; }

        // Se injecta el servicio para consultar productos
        [Inject]
        public IProductService ProductService { get; set; }

        // Se injecta el servicio para trabajar con el carrito de compras
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        // El productoDTO
        public ProductDto Product { get; set; }
        
        // Almacena el mensaje de error
        public string ErrorMessage { get; set; }

        // Permite llevar al usuario al menu correspondiente
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// Método que se sobreescribe para que se carguen los datos del producto al momento de iniciar el componente
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Product = await ProductService.GetItem(Id); 
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        /// <summary>
        /// Metódo asociado al botón que permite agregar un produncto al carrito de compras
        /// </summary>
        /// <param name="cartItemToAddDto"></param>
        /// <returns></returns>
        protected async Task AddToCart_Click(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var cartItemDto = await ShoppingCartService.AddItem(cartItemToAddDto);
                NavigationManager.NavigateTo("/ShoppingCart"); // Redirecciona al carrito de compras
            }
            catch (Exception)
            {

                throw;
            }
        }

    }   
}
