namespace Vehicles.Interface
{
    public interface IVehicle
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
