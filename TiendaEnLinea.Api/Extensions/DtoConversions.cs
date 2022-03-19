using TiendaEnLinea.Api.Entities;
using TiendaEnLinea.Models.Dtos;

namespace TiendaEnLinea.Api.Extensions
{
    /// <summary>
    /// Clase de tipo extension para convertir a dto
    /// </summary>
    public static class DtoConversions
    {   
        /// <summary>
        /// Convierte un coleccion inumerable de productos a una coleccion inumerable de productosdto.
        /// El primer parametro en un método de extensión debe ser el tipo del objeto en el que
        /// queremos llamar el metodo, precedido de la palabra this
        /// </summary>
        /// <param name="products"></param>
        /// <param name="productCategories"></param>
        /// <returns></returns>
        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products, IEnumerable<ProductCategory> productCategories)
        {
            // Se hace un join de productos con categoria de productos usando linq para obter los productos por categoria
            // El resultado del join se pasa a una lista de tipo ProductDto
            return (from product in products
                    join productCategory in productCategories
                    on product.CategoryId equals productCategory.Id
                    select new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        CategoryId = productCategory.Id,
                        CategoryName = productCategory.Name,
                    }).ToList();
        }
    }
}
