using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Application.DTOs.TMDb
{
    public class TmdbGenreResponseDto
    {
        public List<TmdbGenreDto> Genres { get; set; } = new List<TmdbGenreDto>();
    }
}
