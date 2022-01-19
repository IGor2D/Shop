using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using shop.data;
using Shop.Core.Domain;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.ApplicationServices.Services
{
    public class CarServices : ICarService
    {
        private readonly ShopDbcontext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IFileServices _fileServices;
        public CarServices
            (
            ShopDbcontext context,
            IWebHostEnvironment env,
            IFileServices fileServices
            )
        {
            _context = context;
            _env = env;
            _fileServices = fileServices;
        }

        public async Task<Car> Add(CarDto dto)
        {
            Car car = new Car();
            car.Id = Guid.NewGuid();
            car.Name = dto.Name;
            car.Description = dto.Description;
            car.Value = dto.Value;
            car.Color = dto.Color;
            car.CreatedAt = DateTime.Now;
            car.ModifieAt = DateTime.Now;
            _fileServices.ProcessUploadFile(dto, car);

            await _context.Car.AddAsync(car);
            await _context.SaveChangesAsync();
            return car;
        }


        public async Task<Car> Delete(Guid id)
        {
            var carId = await _context.Car
                .Include(x => x.ExistingFilePaths)
                .FirstOrDefaultAsync(x => x.Id == id);

            var photos = await _context.ExistingFilePath
                .Where(x => x.CarId == id)
                .Select(y => new ExistingFilePathDto
                {
                    Id = y.Id,
                    ExistingFilePat = y.FilePath,
                    CarId = y.CarId
                })
                .ToArrayAsync();

            await _fileServices.RemoveImages(photos);
            _context.Car.Remove(carId);
            await _context.SaveChangesAsync();
            return carId;
        }


        public async Task<Car> Update(CarDto dto)
        {
            Car car = new Car();
            car.Id = dto.Id;
            car.Name = dto.Name;
            car.Description = dto.Description;
            car.Value = dto.Value;
            car.Color = dto.Color;
            car.CreatedAt = dto.CreatedAt;
            car.ModifieAt = DateTime.Now;
            _fileServices.ProcessUploadFile(dto, car);

            _context.Car.Update(car);
            await _context.SaveChangesAsync();
            return car;
        }


        public async Task<Car> GetAsync(Guid id)
        {
            var result = await _context.Car
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
