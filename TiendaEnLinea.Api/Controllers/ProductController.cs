using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLinea.Api.Extensions;
using TiendaEnLinea.Api.Repositories.Contracts;
using TiendaEnLinea.Models.Dtos;

namespace TiendaEnLinea.Api.Controllers
{   
    /// <summary>
    /// Clase que representa el controlador asociado al producto
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        /// <summary>
        /// Constructor del controlador, recibe por inyección el repositorio del producto
        /// </summary>
        /// <param name="productRepository"> Parametro de tipo solo lectura </param>
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Retorna la lista de items. Al retornar un ActionResult, trae no solo la data solicitada, sino tambien un estado de respuesta.
        /// Se importo como referencia de proyecto los Dto.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {
                var products = await this.productRepository.GetItems();
                var productCategories = await this.productRepository.GetCategories();

                // Si los productos o las categorias es nula retorna un mensaje de codigo 404
                if (products == null || productCategories == null)
                    return NotFound();
                else
                {
                    // Se obtienen la lista de productos dto con su conversión correspondiente y su join con categorias desde la extension creada
                    var productDtos = products.ConvertToDto(productCategories);

                    // Se retornan los productos con un mensaje de código 200
                    return Ok(productDtos);
                }
                
            }
            catch (Exception)
            {
                // En caso de presentarse un error se enviara un mensaje de código 500
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo la información de la base de datos");
            }
        }
    }
}
