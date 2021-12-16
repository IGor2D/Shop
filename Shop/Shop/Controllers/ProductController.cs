using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop.data;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
using Shop.Models.Product;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShopDbcontext _context;
        private readonly IProductService _productService;
        public ProductController
            (
            ShopDbcontext context,
            IProductService productService
            )
        {
            _context = context;
            _productService = productService;
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
            return View("Edit", model);
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
                ModifieAt = model.ModifieAt,
                Files = model.Files,
                ExistingFilePaths = model.ExistingFilePaths.Select(x => new ExistingFilePathDto
                {
                    Id = x.PhotoId,
                    ExistingFilePat = x.FilePath,
                    ProductId = x.ProductId
                }).ToArray()
            };

            var result = await _productService.Add(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.Delete(id);
            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
            {
                return View(null);
            }
            var photos = await _context.ExistingFilePath
                .Where(x => x.ProductId == id)
                .Select(y => new ExistingFilePathViewModel
                {
                    FilePath = y.FilePath,
                    PhotoId = y.Id
                })
                .ToArrayAsync();

            var model = new ProductViewModel();
            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Value = product.Value;
            model.Weight = product.Weight;
            model.ModifieAt = product.ModifieAt;
            model.CreatedAt = product.CreatedAt;
            model.ExistingFilePaths.AddRange(photos);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            var dto = new ProductDto()
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                Value = model.Value,
                Weight = model.Weight,
                CreatedAt = model.CreatedAt,
                ModifieAt = model.ModifieAt,
                Files = model.Files,
                ExistingFilePaths = model.ExistingFilePaths.Select(x => new ExistingFilePathDto
                {
                    Id = x.PhotoId,
                    ExistingFilePat = x.FilePath,
                    ProductId = x.ProductId
                }).ToArray()
            };
            var result = await _productService.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), model);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveImage(ExistingFilePathViewModel model)
        {
            var dto = new ExistingFilePathDto()
            {
                Id = model.PhotoId
            };
            var photo = await _productService.RemoveImage(dto);
            if (photo == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
