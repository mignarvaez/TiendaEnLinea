using TiendaEnLinea.Models.Dtos;
using TiendaEnLinea.Web.Services.Contracts;
using System.Net.Http.Json;

namespace TiendaEnLinea.Web.Services
{
    /// <summary>
    /// Clase que implementa el servicio que consulta los productos en la API
    /// </summary>
    public class ProductService:IProductService
    {   
        private readonly HttpClient httpClient;

        /// <summary>
        /// Constructor que injecta el cliente http.
        /// </summary>
        /// <param name="httpClient"></param>
        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ProductDto> GetItem(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Product/{id}");

                // Si la respuesta es exitosa se verifica si tiene contenido
                if (response.IsSuccessStatusCode)
                {
                    // Si no tiene contenido se retorna el valor por defecto de dtoproducto, que es null
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductDto);
                    }

                    // Retorna el producto en el formato adecuado
                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }
                else
                {
                    // En caso de no recibir una respuesta exitosa genera una excepción con el mensaje de la misma
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                // Log Exception
                throw;
            }
        }

        /// <summary>
        /// Método que obtiene los items desde la api
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDto>> GetItems()
        {
            try
            {
                // Realiza la petición a la API
                var response = await this.httpClient.GetAsync("api/Product");
                
                // Valida que se genera una respuesta exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Si la resuesta no tiene contenido se retorna un enumerable vacio
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
                }
                else
                {
                    // En caso de no recibir una respuesta exitosa genera una excepción con el mensaje de la misma
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }

            }
            catch (Exception)
            {
                // Log Exception
                throw;
            }
        }
    }
}
