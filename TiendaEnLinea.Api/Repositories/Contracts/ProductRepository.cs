using Microsoft.EntityFrameworkCore;
using TiendaEnLinea.Api.Data;
using TiendaEnLinea.Api.Entities;

namespace TiendaEnLinea.Api.Repositories.Contracts
{
    /// <summary>
    /// Clase que representa el repositorio para un producto. Implementa la interfaz asociada
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly TiendaEnLineaDbContext tiendaEnLineaDbContext;

        /// <summary>
        /// Constructor al que se le injecta el dbcontext tiendaEnLineaDbContext para que pueda usar la conexion a la base de datos
        /// </summary>
        /// <param name="tiendaEnLineaDbContext"> Es un parametro de solo lectura </param>
        public ProductRepository(TiendaEnLineaDbContext tiendaEnLineaDbContext)
        {
            this.tiendaEnLineaDbContext = tiendaEnLineaDbContext;
        }

        /// <summary>
        /// Retorna las categorias de productos del sistema
        /// </summary>
        /// <returns>Las categorias de productos del sistema</returns>
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
           var categories = await this.tiendaEnLineaDbContext.ProductCategories.ToListAsync();
           return categories;
        }

        public Task<IEnumerable<ProductCategory>> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retorna la colección de productos al cliente que lo llama, en este caso un blazor component
        /// </summary>
        /// <returns>La colección de productos</returns>
        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await this.tiendaEnLineaDbContext.Products.ToListAsync();
            return products;
        }
    }
}
