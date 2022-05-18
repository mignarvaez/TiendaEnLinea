using TiendaEnLinea.Api.Entities;

namespace TiendaEnLinea.Api.Repositories.Contracts
{
    /// <summary>
    /// Interfaz que representa el repositorio asociado al producto. Un repositorio se encarga de gestionar toda la lógica asociada al acceso de datos
    /// </summary>
    public interface IProductRepository
    {
        //Estos métodos seran asincronos y retornaran objetos task genericos
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetItem(int id);
        Task<ProductCategory> GetCategory(int id);

    }
}
