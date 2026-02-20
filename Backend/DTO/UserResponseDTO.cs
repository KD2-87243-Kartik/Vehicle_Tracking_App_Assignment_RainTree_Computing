namespace Backend.DTOs
{
    public class UserResponseDto
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Organization { get; set; }
        public string EmailAddress { get; set; }
        public string Location { get; set; }
    }
}