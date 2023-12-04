using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARpe22ShopEvert.Core.Domain;
using TARpe22ShopEvert.Core.Dto;
using TARpe22ShopEvert.Core.ServiceInterface;
using TARpe22ShopEvert.Data;

namespace TARpe22ShopEvert.ApplicationServices.Services
{
    public class CarsServices : ICarsServices
    {
        private readonly TARpe22ShopEvertContext _context;
        private readonly IFilesServices _filesServices;
        public CarsServices
            (
            TARpe22ShopEvertContext context,
            IFilesServices filesServices
            )
        {
            _context = context;
            _filesServices = filesServices;
        }
        public async Task<Car> Create(CarDto dto)
        {
            Car car = new();

            car.Id = Guid.NewGuid();
            car.Mark = dto.Mark;
            car.Model = dto.Model;
            car.Price = dto.Price;
            car.IsNew = dto.IsNew;
            car.HorsePower = dto.HorsePower;
            car.CreatedAt = DateTime.Now;
            _filesServices.FilesToApiCar(dto, car);


            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return car;
        }
        public async Task<Car> Delete(Guid id)
        {
            var carId = await _context.Cars
                .Include(x => x.FilesToApiCar)
                .FirstOrDefaultAsync(x => x.Id == id);
            var images = await _context.FilesToApiCar
                .Where(x => x.CarId == id)
                .Select(y => new FileToApiDto
                {
                    Id = y.Id,
                    CarId = y.CarId,
                    ExistingFilePath = y.ExistingFilePath
                }).ToArrayAsync();
            await _filesServices.RemoveImagesFromApi(images);
            _context.Cars.Remove(carId);
            await _context.SaveChangesAsync();
            return carId;
        }
        public async Task<Car> Update(CarDto dto)
        {
            Car car = new Car();

            car.Id = dto.Id;
            car.Price = dto.Price;
            car.CreatedAt = dto.CreatedAt;
            _filesServices.FilesToApiCar(dto, car);

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            return car;
        }
        public async Task<Car> GetAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
