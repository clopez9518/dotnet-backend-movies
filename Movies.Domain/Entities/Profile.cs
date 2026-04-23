using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string AvatarUrl { get; set; } = null!;

        public bool IsKids { get; set; }
        public List<Movie> MyList { get; set; } = new List<Movie>();

        // 🔗 Relación con User
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
