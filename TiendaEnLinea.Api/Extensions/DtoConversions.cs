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
                        ImageUrl = product.ImageURL,   
                        Price = product.Price,
                        Quantity = product.Quantity,
                        CategoryId = productCategory.Id,
                        CategoryName = productCategory.Name,
                    }).ToList();
        }

        /// <summary>
        /// Convierte un objeto Product en un objeto ProductDto
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productCategory"></param>
        /// <returns></returns>
        public static ProductDto ConvertToDto(this Product product, ProductCategory productCategory)
        {
            // Se crea un productoDto
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageURL,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = productCategory.Id,
                CategoryName = productCategory.Name,
            };
        }

        /// <summary>
        /// Convierte una coleccion de CartItems en una lista de cartitemsdto
        /// </summary>
        /// <param name="cartItems"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        public static IEnumerable<CartItemDto>ConvertToDto(this IEnumerable<CartItem>cartItems, IEnumerable<Product> products)
        {
            return (from cartItem in cartItems
                    join product in products
                    on cartItem.ProductId equals product.Id
                    select new CartItemDto
                    {
                        Id = cartItem.Id,
                        ProductId = cartItem.ProductId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        CartId = cartItem.CartId,
                        Quantity = cartItem.Quantity,
                        TotalPrice = product.Price * cartItem.Quantity,
                    }).ToList();
        }

        public static CartItemDto ConvertToDto(this CartItem cartItem, Product product)
        {
            return new CartItemDto
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL= product.ImageURL,
                Price = product.Price,
                CartId = cartItem.CartId,
                Quantity = cartItem.Quantity,
                TotalPrice = product.Price * cartItem.Quantity,
            };
        }
    }
}
