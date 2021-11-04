using shop.data;
using Shop.Core.Domain;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.ApplicationServices.Services
{
    public class ProductServices : IProductService
    {
        private readonly ShopDbcontext _context;
        public ProductServices
            (
            ShopDbcontext context
            )
        {
            _context = context;
        }

        public async Task<Product> Add(ProductDto dto)
        {
            Product product = new Product();
            product.Id = Guid.NewGuid();
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Value = dto.Value;
            product.Weight = dto.Weight;
            product.CreatedAt = DateTime.Now;
            product.ModifieAt = DateTime.Now;

            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}
