using DAL.DB_Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Services
{
    public class SalesService : ISalesService
    {
        private readonly ShopContext _shopContext;

        public SalesService(ShopContext shopContext) => _shopContext = shopContext;

        public async Task<Sales> AddSale(Sales sale)
        {
            _shopContext.Sales.Add(sale);
            await _shopContext.SaveChangesAsync();

            return sale;
        }

        public async Task<IEnumerable<SalesResponse>> GetAllSales()
        {
            return await (from sales in _shopContext.Sales
                          join client in _shopContext.Clients on sales.ClientId equals client.ClientId
                          join product in _shopContext.Products on sales.ProductId equals product.ProductId
                          select new SalesResponse
                          {
                              Amount = sales.Amount,
                              ClientName = client.Name,
                              ProductName = product.Name,
                          })
                            .ToListAsync();
        }
    }

    public interface ISalesService
    {
        Task<Sales> AddSale(Sales sale);

        Task<IEnumerable<SalesResponse>> GetAllSales();
    }
}
