using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using shop.data;
using Shop.Core.Domain;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Shop.ApplicationServices.Services
{
    public class ProductServices : IProductService
    {
        private readonly ShopDbcontext _context;
        private readonly IWebHostEnvironment _env;
        public ProductServices            
            (
            ShopDbcontext context,
            IWebHostEnvironment env
            )
        {
            _context = context;
            _env = env;
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
            ProcessUploadFile(dto, product);

            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }


        public async Task<Product> Delete (Guid id)
        {
            var productId = await _context.Product
                .Include(x => x.ExistingFilePaths)
                .FirstOrDefaultAsync(x => x.Id == id);
            _context.Product.Remove(productId);
            await _context.SaveChangesAsync();
            return productId;
        }


        public async Task<Product> Update(ProductDto dto)
        {
            Product product = new Product();
            ExistingFilePath file = new ExistingFilePath();
            product.Id = dto.Id;
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Value = dto.Value;
            product.Weight = dto.Weight;
            product.CreatedAt = dto.CreatedAt;
            product.ModifieAt = DateTime.Now;

            if (dto.Files != null)
            {
                file.FilePath = ProcessUploadFile(dto, product);
            }

            _context.Product.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }


        public async Task<Product> GetAsync(Guid id)
        {
            var result = await _context.Product
                .FirstOrDefaultAsync(x => x.Id == id );
            return result;
        }


        public string ProcessUploadFile(ProductDto dto, Product product)
        {
            string uniqueFileName = null;
            if(dto.Files != null && dto.Files.Count > 0)
            {
                if(!Directory.Exists(_env.WebRootPath + "\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_env.WebRootPath + "\\multipleFileUpload\\");
                }

                foreach (var photo in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "multipleFileUpload");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                        ExistingFilePath path = new ExistingFilePath
                        {
                            Id = Guid.NewGuid(),
                            FilePath = uniqueFileName,
                            ProductId = product.Id
                        };
                        _context.ExistingFilePath.Add(path);
                    }
                }
            }
            return uniqueFileName;
        }
        public async Task<ExistingFilePath> RemoveImage(ExistingFilePathDto dto)
        {
            var photoId = await _context.ExistingFilePath
                .FirstOrDefaultAsync(x => x.Id == dto.Id);
            _context.ExistingFilePath.Remove(photoId);
            await _context.SaveChangesAsync();
            return photoId;
        }
    }
}
