namespace Backend.DTOs
{
    public class VehicleResponseDto
    {
        public int VehicleID { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public int ManufacturingYear { get; set; }
        public string ModelNumber { get; set; }
        public int UserID { get; set; }
    }
}