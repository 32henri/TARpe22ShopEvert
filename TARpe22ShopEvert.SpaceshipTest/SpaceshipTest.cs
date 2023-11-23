using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARpe22ShopEvert.Core.Dto;
using Xunit;

namespace TARpe22ShopEvert.SpaceshipTest
{
    public class SpaceshipTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptySpaceship_WhenReturnResult()
        {
            string guid = Guid.NewGuid().ToString();

            SpaceshipDto spaceship = new SpaceshipDto();

            spaceship.Id = Guid.Parse(guid);
            spaceship.Price = 100;
            spaceship.Type = "rocket";
            spaceship.Name = "X ae A 12";
            spaceship.Description = "Description";
            spaceship.FuelType = "Cowfarts";
            spaceship.FuelCapacity = 100;
        }
    }
}
