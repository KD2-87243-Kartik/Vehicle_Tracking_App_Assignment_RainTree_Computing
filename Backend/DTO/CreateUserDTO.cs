namespace Backend.DTOs
{
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Organization { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string Location { get; set; }
    }
}