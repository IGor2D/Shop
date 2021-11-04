using Microsoft.AspNetCore.Mvc;
using shop.data;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
using Shop.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShopDbcontext _context;
        private readonly IProductService _product;
        public ProductController
            (
            ShopDbcontext context,
            IProductService product
            )
        {
            _context = context;
            _product = product;
        }
        [HttpGet]

            public IActionResult Index()
        {
            var result = _context.Product
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new ProductListItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Value = x.Value,
                    Weight = x.Weight
                });
            return View(result);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ProductViewModel model = new ProductViewModel();
            return View("Edit",model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            var dto = new ProductDto()
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                Value = model.Value,
                Weight = model.Weight,
                CreatedAt = model.CreatedAt,
                ModifieAt = model.ModifieAt
            };

            var result = await _product.Add(dto);
            if(result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction();
        }
    }
}
