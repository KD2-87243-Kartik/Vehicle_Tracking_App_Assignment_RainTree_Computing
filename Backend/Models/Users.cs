namespace Backend.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Organization { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string Location { get; set; }
        public string PhotoPath { get; set; }

        // Navigation Property
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}