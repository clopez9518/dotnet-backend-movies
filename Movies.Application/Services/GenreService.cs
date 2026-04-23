

using AutoMapper;
using Movies.Application.DTOs.Genre;
using Movies.Application.Interfaces;
using Movies.Domain.Entities;
using Movies.Application.Exceptions;

namespace Movies.Application.Services
{
    public class GenreService : IGenreService
    {
        private IGenreRepository _genreRepository;
        private IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<GenreDto> Create(CreateGenreDto dto)
        {
            var genre = _mapper.Map<Genre>(dto);
            await _genreRepository.Add(genre);
            await _genreRepository.SaveChangesAsync();

            return _mapper.Map<GenreDto>(genre);
        }

        public async Task<IEnumerable<GenreDto>> GetAll()
        {
            var genres = await _genreRepository.GetAll();
            return genres.Select(g => _mapper.Map<GenreDto>(g));
        }

        public async Task<GenreDto> GetById(int id)
        {
            var genre = await _genreRepository.Get(id) ??
                throw new NotFoundException("Genre not found", "GENRE_NOT_FOUND");


            return _mapper.Map<GenreDto>(genre);
        }

        public async Task<GenreDto> Update(int id, UpdateGenreDto dto)
        {
            var genre = await _genreRepository.Get(id) ??
                throw new NotFoundException("Genre not found", "GENRE_NOT_FOUND");

            genre = _mapper.Map<UpdateGenreDto,Genre>(dto, genre);
            _genreRepository.Update(genre);
            await _genreRepository.SaveChangesAsync();

            return _mapper.Map<GenreDto>(genre);

        }
    }
}
