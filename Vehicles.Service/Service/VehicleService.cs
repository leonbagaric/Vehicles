using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vehicles.Interface;
using Vehicles.Models;

namespace Vehicles.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VehicleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Create
        public async Task CreateModelAsync(VehicleModelDTO dto)
        {
            var model = _mapper.Map<VehicleModel>(dto);
            await _context.VehicleModels.AddAsync(model);
            await _context.SaveChangesAsync();
        }
        public async Task CreateMakeAsync(VehicleMakeDTO dto)
        {
            var make = _mapper.Map<VehicleMake>(dto);
            await _context.VehicleMakes.AddAsync(make);
            await _context.SaveChangesAsync();
        }

        //Read
        public async Task<IEnumerable<VehicleModelDTO>> GetAllModelsAsync(string search, string sort, int page, int pageSize, int? makeId = null)
        {
            var query = _context.VehicleModels.Include(m => m.Make).AsQueryable();


            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(model =>
                    model.Name.Contains(search) ||
                    model.Abrv.Contains(search) ||
                    model.Make.Name.Contains(search) ||
                    model.Make.Abrv.Contains(search));
            }

            if (makeId.HasValue)
            {
                query = query.Where(m => m.MakeId == makeId.Value);
            }


            query = sort switch
            {
                //fullName = makeName + ...  (isti sort)
                "name" => query.OrderBy(m => m.Make.Name),
                "name_descending" => query.OrderByDescending(m => m.Make.Name),
                "makeName" => query.OrderBy(m => m.Make.Name),
                "makeName_descending" => query.OrderByDescending(m => m.Make.Name),
                
                "modelName" => query.OrderBy(m => m.Name),
                "modelName_descending" => query.OrderByDescending(m => m.Name),
                "abrv" => query.OrderBy(m => m.Abrv),
                "abrv_descending" => query.OrderByDescending(m => m.Abrv),
                "makeAbrv" => query.OrderBy(m => m.Make.Abrv),
                "makeAbrv_descending" => query.OrderByDescending(m => m.Make.Abrv),
                _ => query.OrderBy(m => m.Id)
            };

            var results = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize+1)
                .ToListAsync();

            return _mapper.Map<IEnumerable<VehicleModelDTO>>(results);
        }
        public async Task<IEnumerable<VehicleMakeDTO>> GetAllMakesAsync(string search, string sort, int page, int pageSize)
        {
            var query = _context.VehicleMakes.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(make =>
                    make.Name.ToLower().Contains(search.ToLower()) ||
                    make.Abrv.ToLower().Contains(search.ToLower()));
            }

            query = sort switch
            {
                "name" => query.OrderBy(m => m.Name),
                "name_descending" => query.OrderByDescending(m => m.Name),
                "abrv" => query.OrderBy(m => m.Abrv),
                "abrv_descending" => query.OrderByDescending(m => m.Abrv),
                _ => query.OrderBy(m => m.Id)
            };

            var results = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize +1)
                .ToListAsync();

            return _mapper.Map<IEnumerable<VehicleMakeDTO>>(results);
        }

        public async Task<VehicleModelDTO> GetModelByIdAsync(int id)
        {
            var modelById = await _context.VehicleModels.Include(m => m.Make).FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<VehicleModelDTO>(modelById);
        }

        public async Task<VehicleMakeDTO> GetMakeByIdAsync(int id)
        {
            var makeById = await _context.VehicleMakes.FindAsync(id);
            return _mapper.Map<VehicleMakeDTO>(makeById);
        }
        //Update
        public async Task UpdateModelAsync(VehicleModelDTO model)
        {
            var foundModel = await _context.VehicleModels.FindAsync(model.Id);
            if (foundModel == null) return;

            _mapper.Map(model, foundModel);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateMakeAsync(VehicleMakeDTO make)
        {
            var foundMake = await _context.VehicleMakes.FindAsync(make.Id);
            if (foundMake == null) return;

            _mapper.Map(make, foundMake);
            await _context.SaveChangesAsync();
        }
        //Delete
        public async Task DeleteMakeAsync(int id)
        {
            var foundMake = await _context.VehicleMakes.FindAsync(id);
            if (foundMake == null) return;

            _context.VehicleMakes.Remove(foundMake);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteModelAsync(int id)
        {
            var foundModel = await _context.VehicleModels.FindAsync(id);
            if (foundModel == null) return;

            _context.VehicleModels.Remove(foundModel);
            await _context.SaveChangesAsync();
        }
    }
}
