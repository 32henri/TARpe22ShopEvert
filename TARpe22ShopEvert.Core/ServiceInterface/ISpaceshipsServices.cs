using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARpe22ShopEvert.Core.Domain;
using TARpe22ShopEvert.Core.Dto;

namespace TARpe22ShopEvert.Core.ServiceInterface
{
    public interface ISpaceshipsServices
    {
        Task<Spaceship> Create(SpaceshipDto dto);
        //Task<Spaceship> GetUpdate(Guid id);         - not needed
        Task<Spaceship> Update(SpaceshipDto dto);
        Task<Spaceship> Delete(Guid Id);
        Task<Spaceship> GetAsync(Guid Id);
    }
}
