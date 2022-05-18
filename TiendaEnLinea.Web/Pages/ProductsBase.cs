using Microsoft.AspNetCore.Components;
using TiendaEnLinea.Models.Dtos;
using TiendaEnLinea.Web.Services.Contracts;

namespace TiendaEnLinea.Web.Pages
{
    /// <summary>
    /// Clase base que usara el componente de productos
    /// </summary>
    public class ProductsBase:ComponentBase
    {
        // Se injecta el servicio que trae los productos
        [Inject]
        public IProductService ProductService { get; set; }

        // La colección de productos
        public IEnumerable<ProductDto> Products { get; set; }

        // Almacena el mensaje de error
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Se sobreescribe el método OnInitalizedAsync para que al iniciar el componente traiga los productos de la API
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Products = await ProductService.GetItems();
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
            
        }

        /// <summary>
        /// Método que retorna los productos agrupados por categoria
        /// </summary>
        /// <returns></returns>
        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
		{
            return from product in Products
            group product by product.CategoryId into prodByCatGroup
            orderby prodByCatGroup.Key
            select prodByCatGroup;
        }

        /// <summary>
        /// Retorna el nombre de la categoria de productos que llega por parámetro. 
        /// Trae el nombre de la categoria según corresponda para cada producto igualando el valor de la llave del producto parametro con el de los productos agrupados por categoria 
        /// </summary>
        /// <param name="groupedProductDtos"></param>
        /// <returns></returns>
        protected String GetCategoryName(IGrouping<int, ProductDto>groupedProductDtos)
		{
            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;
		}
    }
}
