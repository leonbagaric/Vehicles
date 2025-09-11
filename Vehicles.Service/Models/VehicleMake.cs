using System.ComponentModel.DataAnnotations;
using Vehicles.Interface;

namespace Vehicles.Models
{
    public class VehicleMake : IVehicle
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Abrv {  get; set; }


        public ICollection<VehicleModel> Model { get; set; }
    }


    public class VehicleMakeDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Abbreviation")]
        public string? Abrv { get; set; }
    }

}
