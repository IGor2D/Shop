using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop.data;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
using Shop.Models.Car;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class CarController : Controller
    {
        private readonly ShopDbcontext _context;
        private readonly ICarService _carService;
        private readonly IFileServices _fileServices;
        public CarController
            (
            ShopDbcontext context,
            ICarService carService,
            IFileServices fileServices
            )
        {
            _context = context;
            _carService = carService;
            _fileServices = fileServices;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.Car
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new CarListItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Value = x.Value,
                    Color = x.Color
                });
            return View(result);
        }


        [HttpGet]
        public IActionResult Add()
        {
            CarViewModel model = new CarViewModel();
            return View("Edit", model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(CarViewModel model)
        {
            var dto = new CarDto()
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                Value = model.Value,
                Color = model.Color,
                CreatedAt = model.CreatedAt,
                ModifieAt = model.ModifieAt,
                Files = model.Files,
                ExistingFilePaths = model.ExistingFilePaths.Select(x => new ExistingFilePathDto
                {
                    Id = x.PhotoId,
                    ExistingFilePat = x.FilePath,
                    CarId = x.CarId
                }).ToArray()
            };

            var result = await _carService.Add(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carService.Delete(id);
            if (car == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var car = await _carService.GetAsync(id);
            if (car == null)
            {
                return View(null);
            }
            var photos = await _context.ExistingFilePath
                .Where(x => x.CarId == id)
                .Select(y => new ExistingFilePathViewModel
                {
                    FilePath = y.FilePath,
                    PhotoId = y.Id
                })
                .ToArrayAsync();

            var model = new CarViewModel();
            model.Id = car.Id;
            model.Name = car.Name;
            model.Description = car.Description;
            model.Value = car.Value;
            model.Color = car.Color;
            model.ModifieAt = car.ModifieAt;
            model.CreatedAt = car.CreatedAt;
            model.ExistingFilePaths.AddRange(photos);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CarViewModel model)
        {
            var dto = new CarDto()
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                Value = model.Value,
                Color = model.Color,
                CreatedAt = model.CreatedAt,
                ModifieAt = model.ModifieAt,
                Files = model.Files,
                ExistingFilePaths = model.ExistingFilePaths.Select(x => new ExistingFilePathDto
                {
                    Id = x.PhotoId,
                    ExistingFilePat = x.FilePath,
                    CarId = x.CarId
                }).ToArray()
            };
            var result = await _carService.Update(dto);
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
            var photo = await _fileServices.RemoveImage(dto);
            if (photo == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
