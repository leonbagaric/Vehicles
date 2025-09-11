using System;
using System.Collections.Generic;

namespace Vehicles.Data;

public partial class VehicleMakeDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Abrv { get; set; }
}
