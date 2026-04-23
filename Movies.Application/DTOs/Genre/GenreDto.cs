using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Application.DTOs.Genre
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int IdTmdb { get; set; }

    }
}
