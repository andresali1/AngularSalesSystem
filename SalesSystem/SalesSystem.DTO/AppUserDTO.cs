namespace SalesSystem.DTO
{
    public class AppUserDTO
    {
        public int UserId { get; set; }

        public string? CompleteName { get; set; }

        public string? Email { get; set; }

        public int? RoleId { get; set; }

        public string? RoleDescription { get; set; }

        public string? Pass { get; set; }

        public int? IsActive { get; set; }
    }
}
