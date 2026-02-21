namespace Backend.DTO
{
    public class UserUpdateDTO
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Organization { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string Location { get; set; }
        public string? PhotoPath { get; set; }
    }
}