using API.Entities;

namespace API.Models
{
    public class AdminAuthenticateResponse
    {
        public int Id { get; set; }
        public string HashId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public string Role { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public AdminAuthenticateResponse(Admin admin, string _token)
        {
            Id = admin.Id;
            HashId = admin.HashId;
            Name = admin.Name;
            Email = admin.Email;
            Role = admin.Role;
            Token = _token;
        }
    }
}
