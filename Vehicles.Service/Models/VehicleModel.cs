using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicles.Interface;

namespace Vehicles.Models
{
    public class VehicleModel : IVehicle
    {
        [Key]
        public int Id { get; set; }

        public int MakeId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(20)]
        [Display(Name = "Abbreviation")]
        public string Abrv { get; set; }

        [ForeignKey("MakeId")]
        public VehicleMake Make { get; set; }
    }

    public class VehicleModelDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Make is required")]

        public int MakeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Abbreviation")]
        public string? Abrv { get; set; }

        [ValidateNever]
        public string MakeAbrv { get; set; }

        [ValidateNever]
        public string MakeName { get; set; }

        [Display(Name = "Full Name")]
        [ValidateNever]
        public string FullName => $"{MakeName} {Name}";
    }
}
