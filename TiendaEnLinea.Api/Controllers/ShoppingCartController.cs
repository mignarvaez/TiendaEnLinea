using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLinea.Api.Extensions;
using TiendaEnLinea.Api.Repositories.Contracts;
using TiendaEnLinea.Models.Dtos;
/// <summary>
/// Controlador que se encarga del carrito de compras
/// </summary>
namespace TiendaEnLinea.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        // Elemento que representa el repositorio del carrito de compras
        private readonly IShoppingCartRepository shoppingCartRepository;

        // Elemento que representa el repositorio de productos
        private readonly IProductRepository productRepository;

        /// <summary>
        /// Constructor del controlador
        /// </summary>
        /// <param name="shoppingCartRepository"></param>
        /// <param name="productRepository"></param>
        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Método que retorna la lista de articulos del carrito de compras según usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userId)
        {
            try
            {
                // Busca los items
                var cartItems = await this.shoppingCartRepository.GetItems(userId);

                // Si no encuentra items retorna un mensaje no content código 204
                if (cartItems == null)
                {
                    return NoContent();
                }

                // Obtiene los productos
                var products = await this.productRepository.GetItems();

                // Si no hay productos lanza una excepción
                if (products == null)
                {
                    throw new Exception("No hay productos en el sistema");
                }

                // Si hay productos genera la lista de items dto y los retorna con mensaje código 200(ok)
                var cartItemsDto = cartItems.ConvertToDto(products);
                return Ok(cartItemsDto);
            }
            catch (Exception ex)
            {
                // Se muestra el mensaje de la excepción con el código 500
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método que obtiene un item del carrito de compras según su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDto>> GetItem(int id)
        {
            try
            {
                // Busca el articulo, si no lo encuentra retorna un mensaje apropiado
                var cartItem = await this.shoppingCartRepository.GetItem(id);
                if (cartItem == null)
                {
                    return NotFound();
                }
                // Si el articulo existe, busca el producto asociado al mismo
                var product = await productRepository.GetItem(cartItem.ProductId);

                // Si el producto no existe lanza una excepción indicando la irregularidad
                if (product == null)
                {
                    throw new Exception("Ocurrio un error al obtener el producto.");
                }

                // Si el producto existe realiza la conversión a un itemdto y lo retorna con mensaje 200
                var cartItemDto = cartItem.ConvertToDto(product);
                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método que permite agregar un producto a la lista de articulos del carrito de compra.
        /// </summary>
        /// <param name="cartItemToAddDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CartItemDto>> PostItem([FromBody] CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                // Agrega el producto
                var newCarItem = await this.shoppingCartRepository.AddItem(cartItemToAddDto);

                // Si no se agrego se retorna una respuesta 204 no content
                if (newCarItem == null)
                {
                    return NoContent();
                }

                // Busca el producto asociado 
                var product = await productRepository.GetItem(newCarItem.ProductId);

                // Si el producto no se encuentra se lanza un error indicando la situacion y la id asociada
                if (product == null)
                {
                    throw new Exception($"Ocurrio un error al obtener el producto(productId:({cartItemToAddDto.ProductId}).)");
                }

                // Se crea un objeto dto para enviar como respuesta
                var newCartItemDto = newCarItem.ConvertToDto(product);

                // Se recomienda al crear un objeto con post retornar la uri del mismo, en este caso especificando el metodo GetItem
                // Con createdataction
                return CreatedAtAction(nameof(GetItem), new { id = newCartItemDto.Id }, newCartItemDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método que representa el endpoint para eliminar un item 
        /// </summary>
        /// <param name="id">El id del item</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CartItemDto>> DeleteItem(int id)
        {
            try
            {
                // Intena eliminar el item si no lo encuentra retorna not found 
                var carItem = await this.shoppingCartRepository.DeleteItem(id);
                if (carItem == null)
                {
                    return NotFound();
                }

                // Lo que se le muestra al cliente no es el item como tal sino un
                // objeto DTO relacionado a ese producto. Primero se busca el producto
                var product = await this.productRepository.GetItem(carItem.ProductId);

                // Si el producto es nulo se devuelve not found
                if (product == null)
                    return NotFound();

                // Se transforma el producto en un DTO
                var carItemDto = carItem.ConvertToDto(product);

                // Se retorna un mensaje de tipo ok(Código 200) con el dto
                return Ok(carItemDto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
