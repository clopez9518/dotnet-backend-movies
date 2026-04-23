

namespace Movies.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastLoginAt { get; set; }

        // 🔗 Relación: un usuario puede tener varios perfiles
        public ICollection<Profile> Profiles { get; set; } = new List<Profile>();
        public List<RefreshToken>? RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
