using Vehicles.Models;

namespace Vehicles.Interface
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleModelDTO>> GetAllModelsAsync(string search, string sort, int page, int pageSize, int? makeId);
        Task<IEnumerable<VehicleMakeDTO>> GetAllMakesAsync(string search, string sort, int page, int pageSize);

        Task<VehicleModelDTO> GetModelByIdAsync(int id);
        Task CreateModelAsync(VehicleModelDTO model);
        Task UpdateModelAsync(VehicleModelDTO model);
        Task DeleteModelAsync(int id);

        Task<VehicleMakeDTO> GetMakeByIdAsync(int id);
        Task CreateMakeAsync(VehicleMakeDTO make);
        Task UpdateMakeAsync(VehicleMakeDTO make);
        Task DeleteMakeAsync(int id);
    }
}
