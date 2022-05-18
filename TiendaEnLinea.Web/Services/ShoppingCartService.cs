using System.Net.Http.Json;
using TiendaEnLinea.Models.Dtos;
using TiendaEnLinea.Web.Services.Contracts;

namespace TiendaEnLinea.Web.Services
{
    /// <summary>
    /// Clase que implementa el servicio para interactuar con el carrito de compras
    /// </summary>
    public class ShoppingCartService : IShoppingCartService
    {
        // Cliente http inyectable
        private readonly HttpClient httpClient;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="httpClient"></param>
        public ShoppingCartService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        
        /// <summary>
        /// Método que permite agregar un item al carrito de compras
        /// </summary>
        /// <param name="cartItemToAddDto"></param>
        /// <returns></returns>
        public async Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            try
            {   // Se intenta pasar a la api el objeto cartItemToAddDto
                var response = await httpClient.PostAsJsonAsync<CartItemToAddDto>("api/ShoppingCart",cartItemToAddDto);

                // Si el mensaje es ok 
                if (response.IsSuccessStatusCode)
                {
                    // Se retorna null si la api no retorno nada
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(CartItemDto);
                    }

                    // En caso de retornar se retorna una representacion del item nuevo creado
                    return await response.Content.ReadFromJsonAsync<CartItemDto>(); 
                }
                // Si la respuesta no es satisfactoria lanza una excepcion con mensaje indicando error y código de respuesta
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {response.StatusCode} Message: {message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método que elimina un item del carrito de compras según su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CartItemDto> DeleteItem(int id)
        {
            try
            {   
                // Intenta eliminar el item
                var response = await httpClient.DeleteAsync($"api/ShoppingCart/{id}");
                if (response.IsSuccessStatusCode)
                {
                    // Si el eliminar fue exitoso se retorna el dto asociado al item
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }

                // Si no fue exitoso se retorna null
                return default(CartItemDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método que obtiene todos los items del carrito segun usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<CartItemDto>> GetItems(int userId)
        {
            try
            {
                // Obtiene los articulos desde la api
                var response = await httpClient.GetAsync($"api/ShoppingCart/{userId}/GetItems");

                // Valida que la respuesta sea exitosa y que sea diferente de vacio
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CartItemDto>().ToList();
                    }
                    return await response.Content.ReadFromJsonAsync<List<CartItemDto>>();
                }
                // En caso de presentares un error genera una excepcion
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($" Http status code: {response.StatusCode} Message: { message }");
                }
            }   
            catch (Exception)
            {

                throw;
            }
        }
    }
}
