using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TiendaEnLinea.Web;
using TiendaEnLinea.Web.Services;
using TiendaEnLinea.Web.Services.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Se especifica la URL de la api como url base
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7058/") });

// Se indica que se usara como dependencia de Injección el productservice
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
await builder.Build().RunAsync();
