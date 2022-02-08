using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop.data;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
using Shop.Models.Spaceship;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class SpaceshipController : Controller
    {
        private readonly ShopDbcontext _context;
        private readonly ISpaceshipService _spaceshipService;

        public SpaceshipController
            (
            ShopDbcontext context,
            ISpaceshipService spaceshipService
            )
        {
            _context = context;
            _spaceshipService = spaceshipService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.Spaceship
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new SpaceshipListItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Model = x.Model,
                    Company = x.Company,
                    Country = x.Country,
                    LaunchDate = x.LaunchDate,
                    CreatedAt = x.CreatedAt
                });
            return View(result);
        }


        [HttpGet]
        public IActionResult Add()
        {
            SpaceshipViewModel model = new SpaceshipViewModel();
            return View("Edit", model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(SpaceshipViewModel model2)
        {
            var dto = new SpaceshipDto()
            {
                Id = model2.Id,
                Name = model2.Name,
                Model = model2.Model,
                Company = model2.Company,
                EnginePower = model2.EnginePower,
                Country = model2.Country,
                LaunchDate = model2.LaunchDate,
                CreatedAt = model2.CreatedAt,
                ModifieAt = model2.ModifieAt,
                Files = model2.Files,
                Image = model2.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    SpaceshipId = x.SpaceshipId
                }).ToArray()
            };

            var result = await _spaceshipService.Add(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", model2);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var spaceship = await _spaceshipService.Delete(id);
            if (spaceship == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var spaceship = await _spaceshipService.GetAsync(id);
            if (spaceship == null)
            {
                return View(null);
            }

            var photos = await _context.FileToDatabase
                .Where(x => x.SpaceshipId == id)
                .Select(y => new ImageViewModel
                {
                    ImageData = y.ImageData,
                    ImageId = y.Id,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData)),
                    SpaceshipId = y.Id,
                }).ToArrayAsync();

            var model = new SpaceshipViewModel();
            model.Id = spaceship.Id;
            model.Name = spaceship.Name;
            model.Model = spaceship.Model;
            model.Company = spaceship.Company;
            model.EnginePower = spaceship.EnginePower;
            model.Country = spaceship.Country;
            model.LaunchDate = spaceship.LaunchDate;
            model.ModifieAt = spaceship.ModifieAt;
            model.CreatedAt = spaceship.CreatedAt;
            model.Image.AddRange(photos);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(SpaceshipViewModel model2)
        {
            var dto = new SpaceshipDto()
            {
                Id = model2.Id,
                Name = model2.Name,
                Model = model2.Model,
                Company = model2.Company,
                EnginePower = model2.EnginePower,
                Country = model2.Country,
                LaunchDate = model2.LaunchDate,
                ModifieAt = model2.ModifieAt,
                CreatedAt = model2.CreatedAt,
                Files = model2.Files,
                Image = model2.Image.Select(x => new FileToDatabaseDto
                {
                   Id = x.ImageId,
                   ImageData = x.ImageData,
                   ImageTitle = x.ImageTitle,
                   SpaceshipId = x.SpaceshipId
                })
            };
            var result = await _spaceshipService.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), model2);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveImage(ImageViewModel file)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = file.ImageId
            };
            var image = await _spaceshipService.RemoveImage(dto);
            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
