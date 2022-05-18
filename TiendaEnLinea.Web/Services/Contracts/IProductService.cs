using TiendaEnLinea.Models.Dtos;

namespace TiendaEnLinea.Web.Services.Contracts
{
    /// <summary>
    /// Interfaz que representa el servicio que interactua con la API
    /// </summary>
    public interface IProductService
    {

        Task<IEnumerable<ProductDto>> GetItems();
        Task<ProductDto> GetItem(int id);
    }
}
