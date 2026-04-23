using Movies.Application.DTOs.Genre;


namespace Movies.Application.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAll();
        Task<GenreDto> GetById(int id);

        Task<GenreDto> Create(CreateGenreDto dto);

        Task<GenreDto> Update(int id,UpdateGenreDto dto);
    }
}
