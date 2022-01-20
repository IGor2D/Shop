using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using shop.data;
using Shop.Core.Domain;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Shop.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly ShopDbcontext _context;
        private readonly IWebHostEnvironment _env;
        public FileServices
            (
            ShopDbcontext context,
            IWebHostEnvironment env
            )
        {
            _context = context;
            _env = env;
        }
        public string ProcessUploadFile(ProductDto dto, Product product)
        {
            string uniqueFileName = null;
            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (!Directory.Exists(_env.WebRootPath + "\\multipleFileUpload\\"))
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
                        _context.ExistingFilePath.AddAsync(path);

                    }
                }
            }
            return null;
        }
 
        public string ProcessUploadFile(CarDto dto, Car car)
            {
                string uniqueFileName = null;
                if (dto.Files != null && dto.Files.Count > 0)
                {
                    if (!Directory.Exists(_env.WebRootPath + "\\multipleFileUpload\\"))
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
                                CarId = car.Id,
                            };
                            _context.ExistingFilePath.AddAsync(path);
                        }
                    }
                }
                return null;
            }
            public async Task<ExistingFilePath> RemoveImage(ExistingFilePathDto dto)
            {
                var photoId = await _context.ExistingFilePath
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);
                var filePath = _env.WebRootPath + "\\multipleFileUpload\\" + photoId.FilePath;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                _context.ExistingFilePath.Remove(photoId);
                await _context.SaveChangesAsync();
                return photoId;
            }
            public async Task<ExistingFilePath> RemoveImages(ExistingFilePathDto[] dto)
            {
                foreach (var dtos in dto)
                {
                    var photoId = await _context.ExistingFilePath
                    .FirstOrDefaultAsync(x => x.FilePath == dtos.ExistingFilePat);
                    var filePath = _env.WebRootPath + "\\multipleFileUpload\\" + photoId.FilePath;
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    _context.ExistingFilePath.Remove(photoId);
                    await _context.SaveChangesAsync();
                }
                return null;
            }          
    }
}
