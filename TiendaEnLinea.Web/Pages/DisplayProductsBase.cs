using Microsoft.AspNetCore.Components;
using TiendaEnLinea.Models.Dtos;

namespace TiendaEnLinea.Web.Pages
{
	/// <summary>
	/// Clase base para el componente display products que se encarga de la información a mostrar de los productos
	/// </summary>
	public class DisplayProductsBase:ComponentBase
	{
		// Se declara un lista de productos como un parametro de componente
		[Parameter]
		public IEnumerable<ProductDto> Products { get; set; }
		
	}
}
