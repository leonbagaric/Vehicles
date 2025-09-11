using System;
using System.Collections.Generic;

namespace Vehicles.Data;

public partial class VehicleModelDto
{
    public int Id { get; set; }

    public int MakeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Abrv { get; set; }

    public string MakeAbrv { get; set; } = null!;
}
