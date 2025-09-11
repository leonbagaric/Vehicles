namespace Vehicles.Models
{
    public class IndexHelper
    {
        public IEnumerable<VehicleModelDTO> Models { get; set; }
        public IEnumerable<VehicleMakeDTO> Makes { get; set; }

        public string FilterMakes { get; set; }
        public int PageMakes { get; set; } = 1;
        public string SortMakes { get; set; }
        public bool HasNextPageMakes { get; set; }

        public string FilterModels { get; set; }
        public int PageModels { get; set; } = 1;
        public string SortModels { get; set; }
        public bool HasNextPageModels { get; set; }

        public int PageSize { get; set; } = 5;
        public int? MakeId { get; set; }
    }
}
