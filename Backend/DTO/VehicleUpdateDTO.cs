namespace Backend.DTO
{
    public class VehicleUpdateDTO
    {
        public string? VehicleNumber { get; set; }
        public string? VehicleType { get; set; }
        public string? ChassisNumber { get; set; }
        public string? EngineNumber { get; set; }
        public int? ManufacturingYear { get; set; }
        public double? LoadCarryingCapacity { get; set; }
        public string? MakeOfVehicle { get; set; }
        public string? ModelNumber { get; set; }
        public string? BodyType { get; set; }
        public string? OrganisationName { get; set; }
        public string? DeviceID { get; set; }
    }
}
