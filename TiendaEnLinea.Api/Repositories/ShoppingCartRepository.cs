using Microsoft.EntityFrameworkCore;
using TiendaEnLinea.Api.Data;
using TiendaEnLinea.Api.Entities;
using TiendaEnLinea.Api.Repositories.Contracts;
using TiendaEnLinea.Models.Dtos;

namespace TiendaEnLinea.Api.Repositories
{
    /// <summary>
    /// Clase que implementa lo necesario para gestionar el carro de compras
    /// </summary>
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        // DbContext para trabajar con la base de datos
        private readonly TiendaEnLineaDbContext tiendaEnLineaDbContext;

        /// <summary>
        /// Constructor que inicializa el dbcontext
        /// </summary>
        /// <param name="tiendaEnLineaDbContext"></param>
        public ShoppingCartRepository(TiendaEnLineaDbContext tiendaEnLineaDbContext)
        {
            this.tiendaEnLineaDbContext = tiendaEnLineaDbContext;
        }

        /// <summary>
        /// Método que verifica si existe o no previamente un producto en el carrito de compras
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await this.tiendaEnLineaDbContext.CartItems.AnyAsync(x => x.CartId == cartId && x.ProductId == productId);
        }

        /// <summary>
        /// Método que agrega un producto al carrito de compras
        /// </summary>
        /// <param name="cartItemToAddDto"></param>
        /// <returns></returns>
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {   
            // Si el producto no existe en el carrito de compras se agrega
            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                // Se verifica que exista el producto a agregar y se crea el objeto cartitem correspondiente
                var item = await (from product in this.tiendaEnLineaDbContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      ProductId = product.Id,
                                      Quantity = cartItemToAddDto.Quantity,
                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    // Si el item fue creado se almacena en la tabla de car items
                    var result = await this.tiendaEnLineaDbContext.CartItems.AddAsync(item);
                    await this.tiendaEnLineaDbContext.SaveChangesAsync();
                    // Se retorna la entidad almacenada
                    return result.Entity;
                }
            }

            
            // Si el item no fue creado se retorna null
            return null;
        }

        /// <summary>
        /// Método que elimina un item del carrito de compras
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CartItem> DeleteItem(int id)
        {   
            // Lo busca y si lo encuentra lo elimina
            var item = await this.tiendaEnLineaDbContext.CartItems.FindAsync(id);
            if(item != null)
            {   
                // Lo elimina y con el savechanges hace la operacion en la
                // base de datos.
                this.tiendaEnLineaDbContext.CartItems.Remove(item);
                this.tiendaEnLineaDbContext.SaveChanges();
            }
            return item;
        }

        /// <summary>
        /// Retorna un item del carrito de compras
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in this.tiendaEnLineaDbContext.Carts
                          join cartItem in this.tiendaEnLineaDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Quantity = cartItem.Quantity,
                              CartId = cartItem.CartId,
                          }).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Retorna la lista de items el carrito de compras de un usuario dado  
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await (from cart in this.tiendaEnLineaDbContext.Carts
                          join cartItem in this.tiendaEnLineaDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Quantity = cartItem.Quantity,
                              CartId = cartItem.CartId,
                          }).ToListAsync();
           
        }

        public Task<CartItem> UpdateQuantity(int id, CartItemQuantityUpdateDto cartItemQuantityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
