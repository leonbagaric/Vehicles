using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vehicles.Interface;
using Vehicles.Models;

namespace Vehicles.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(
            string filterModels, string sortModels,  string filterMakes, string sortMakes, int pageModels = 1, int pageMakes = 1,int? makeId = null)
        {
            int pageSize = 5;

            var models = (await _vehicleService.GetAllModelsAsync(filterModels, sortModels, pageModels, pageSize, makeId)).ToList();
            bool hasNextPageModels = models.Count > pageSize;

            var makes = (await _vehicleService.GetAllMakesAsync(filterMakes, sortMakes, pageMakes, pageSize)).ToList();
            bool hasNextPageMakes = makes.Count > pageSize;
            if (hasNextPageMakes)
                makes.RemoveAt(pageSize);

            var helper = new IndexHelper
            {
                Models = models,
                Makes = makes,

                FilterModels = filterModels,
                SortModels = sortModels,
                PageModels = pageModels,
                HasNextPageModels = hasNextPageModels,

                FilterMakes = filterMakes,
                SortMakes = sortMakes,
                PageMakes = pageMakes,
                HasNextPageMakes = hasNextPageMakes,

                PageSize = pageSize,
                MakeId = makeId,
            };

            return View(helper);
        }


        [HttpGet]
        public async Task<IActionResult> ModelDetails(int id)
        {
            var model = await _vehicleService.GetModelByIdAsync(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MakeDetails(int id)
        {
            var make = await _vehicleService.GetMakeByIdAsync(id);
            if (make == null)
                return NotFound();

            return View(make);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateModel()
        {
            var makes = await _vehicleService.GetAllMakesAsync(null, null, 1, 100);
            ViewBag.MakeList = new SelectList(makes, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateModel(VehicleModelDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MakeList = new SelectList(await _vehicleService.GetAllMakesAsync(null, null, 1, 100), "Id", "Name");
                return View(dto);
            }
            
            await _vehicleService.CreateModelAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateMake()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMake(VehicleMakeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await _vehicleService.CreateMakeAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditModel(int id)
        {
            var model = await _vehicleService.GetModelByIdAsync(id);
            if (model == null)
                return NotFound();

            ViewBag.Makes = await _vehicleService.GetAllMakesAsync(null, null, 1, 100);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditMake(int id)
        {
            var make = await _vehicleService.GetMakeByIdAsync(id);
            if (make == null)
                return NotFound();

            return View(make);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditModel(int id, VehicleModelDTO dto)
        {
            if (id != dto.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _vehicleService.UpdateModelAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditMake(int id, VehicleMakeDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            await _vehicleService.UpdateMakeAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            var model = await _vehicleService.GetModelByIdAsync(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost, ActionName("DeleteModel")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteModelConfirmed(int id)
        {
            var model = await _vehicleService.GetModelByIdAsync(id);
            if (model == null)
                return NotFound();

            await _vehicleService.DeleteModelAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMake(int id)
        {
            var make = await _vehicleService.GetMakeByIdAsync(id);
            if (make == null)
                return NotFound();

            return View(make);
        }

        [HttpPost, ActionName("DeleteMake")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMakeConfirmed(int id)
        {
            var make = await _vehicleService.GetMakeByIdAsync(id);
            if (make == null)
                return NotFound();

            await _vehicleService.DeleteMakeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
