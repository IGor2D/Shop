using Shop.Core.Domain;
using Shop.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface ICarService : IApplicationService
    {
        Task<Car> Add(CarDto dto2);
        Task<Car> Delete(Guid id);
        Task<Car> Update(CarDto dto2);
        Task<Car> GetAsync(Guid id);
        //Task<ExistingFilePath> RemoveImage(ExistingFilePathDto dto);
    }
}
