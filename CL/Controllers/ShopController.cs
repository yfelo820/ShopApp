using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using SL.Services;

namespace CL.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService) => _shopService = shopService;

        /// <summary>
        /// EndPoint Utilizado para Registar Ventas de un cliente a un producto
        /// </summary>
        /// <param name="clientId"> Id del Cliente que realiza la compra </param>
        /// <param name="productId"> Id del Producto comprado </param>
        /// <param name="amount"> Cantidad comprada del producto </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterSale(RegisterSaleBody registerSale)
        {
            return Ok(await _shopService.RegisterSale(registerSale.clientId, registerSale.productId, registerSale.amount));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            return Ok(await _shopService.GetAllSales());
        }
    }
}
