using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;
using TARpe22ShopEvert.ApplicationServices.Services;
using TARpe22ShopEvert.Core.Dto;
using TARpe22ShopEvert.Core.ServiceInterface;
using TARpe22ShopEvert.Data;
using TARpe22ShopEvert.Models.Cars;
using TARpe22ShopEvert.Models.RealEstate;
using TARpe22ShopEvert.Models.Spaceship;
using static System.Net.Mime.MediaTypeNames;

namespace TARpe22ShopEvert.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsServices _car;
        private readonly TARpe22ShopEvertContext _context;
        private readonly IFilesServices _filesServices;
        public CarsController
            (
            ICarsServices car,
            TARpe22ShopEvertContext context,
            IFilesServices filesServices
            )
        {
            _car = car;
            _context = context;
            _filesServices = filesServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.Cars
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new CarIndexViewModel
                {
                    Id = x.Id,
                    Mark = x.Mark,
                    IsNew= x.IsNew,
                    HorsePower = x.HorsePower,
                    Model = x.Model,
                    Price = x.Price,
                    CreatedAt = x.CreatedAt,
                });
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CarCreateUpdateViewModel vm = new();
            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = Guid.NewGuid(),
                Mark = vm.Mark,
                Model = vm.Model,
                IsNew = vm.IsNew,
                Price = vm.Price,
                HorsePower = vm.HorsePower,
                CreatedAt = DateTime.Now,
            };
            var result = await _car.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var car = await _car.GetAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.CarId == id)
                .Select(y => new FileToApiViewModelCar
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();
            var vm = new CarCreateUpdateViewModel();

            vm.Id = car.Id;
            vm.Mark = car.Mark;
            vm.Model = car.Model;
            vm.IsNew = car.IsNew;
            vm.Price = car.Price;
            vm.HorsePower = car.HorsePower;
            vm.CreatedAt = DateTime.Now;
            vm.FileToApiViewModelCar.AddRange(images);

            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = (Guid)vm.Id,
                Mark = vm.Mark,
                Model = vm.Model,
                HorsePower = vm.HorsePower,
                IsNew = vm.IsNew,
                Price = vm.Price,
                CreatedAt = vm.CreatedAt,
                FilesToApiDtos = vm.FileToApiViewModelCar
                .Select(z => new FileToApiDto
                {
                    Id = z.ImageId,
                    ExistingFilePath = z.FilePath,
                    CarId = z.CarId,
                }).ToArray()
            };
            var result = await _car.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _car.GetAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.CarId == id)
                .Select(y => new FileToApiViewModelCar
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new CarDetailsViewModel();

            vm.Id = car.Id;
            vm.Price = car.Price;
            vm.Mark = car.Mark;
            vm.Model = car.Model;
            vm.HorsePower = car.HorsePower;
            vm.IsNew = car.IsNew;
            vm.CreatedAt = car.CreatedAt;
            vm.FileToApiViewModelCar.AddRange(images);

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _car.GetAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.CarId == id)
                .Select(y => new FileToApiViewModelCar
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new CarDeleteViewModel();

            vm.Id = car.Id;
            vm.Mark = car.Mark;
            vm.Model = car.Model;
            vm.HorsePower = car.HorsePower;
            vm.IsNew = car.IsNew;
            vm.CreatedAt = car.CreatedAt;
            vm.Price = car.Price;
            vm.FileToApiViewModelCar.AddRange(images);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var car = await _car.Delete(id);
            if (car == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public async Task<IActionResult> RemoveImage(FileToApiViewModelCar vm)
        {
            var dto = new FileToApiDto()
            {
                Id = vm.ImageId
            };
            var image = await _filesServices.RemoveImageFromApi(dto);
            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
