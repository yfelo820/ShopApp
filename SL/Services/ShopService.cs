using DAL.Entities;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Services
{
    public class ShopService : IShopService
    {
        private readonly IClientService _clientService;
        private readonly IProductService _productService;
        private readonly ISalesService _salesService;

        public ShopService(IClientService clientService,
                           IProductService productService,
                           ISalesService salesService)
        {
            _clientService = clientService;
            _productService = productService;
            _salesService = salesService;
        }

        public async Task<IEnumerable<SalesResponse>> GetAllSales()
        {
            return await _salesService.GetAllSales();
        }

        public async Task<bool> RegisterSale(int clientId, int productId, int amount)
        {
            var product = await _productService.GetProductById(productId);

            product.Stock -= amount;

            await _productService.UpdateProduct(product);

            var sale = new Sales 
            {
                ClientId = clientId,
                ProductId = productId,
                Amount = amount                 
            };

            var saleDone = await _salesService.AddSale(sale);

            if(saleDone.SaleId > (int)default)
            {
                return true;
            }

            return false;
        }
    }

    public interface IShopService
    {
        Task<bool> RegisterSale(int clientId, int productId, int amount);

        Task<IEnumerable<SalesResponse>> GetAllSales();
    }
}
