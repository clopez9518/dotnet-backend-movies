
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Movies.Application.DTOs.Movie;
using Movies.Application.DTOs.MovieAdmin;
using Movies.Application.DTOs.TMDb;
using Movies.Application.Interfaces;
using Movies.Domain.Entities;
using Movies.Infrastructure.External.TMDb.Mappers;
using System.Net.Http.Json;

namespace Movies.Infrastructure.External.TMDb.Services
{
    public class TmdbService : ITmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly string? _apiKey;
        private readonly string? _baseUrl;

        public TmdbService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _apiKey = _config["TMDb:ApiKey"];
            _baseUrl = _config["TMDb:BaseUrl"];
        }
        public async Task<IEnumerable<TmdbMovieDto>> GetPopularMoviesAsync(int totalPages)
        {

            var allMovies = new List<TmdbMovieDto>();

            for (int page = 1; page <= totalPages; page++)
            {
                var url = $"{_baseUrl}movie/popular?api_key={_apiKey}&page={page}";
                var response = await _httpClient.GetFromJsonAsync<TmdbResponseDto>(url);

                if (response?.Results != null)
                {
                    allMovies.AddRange(response.Results);
                }
            }

            // Remove duplicates based on movie ID
            allMovies = allMovies
                .GroupBy(m => m.Id)
                .Select(g => g.First())
                .ToList();
            return allMovies;
        }

        public async Task<TmdbMovieResponseDto?> GetMovieDetailsAsync(int movieId)
        {
            try
            {
                var url = $"{_baseUrl}movie/{movieId}?api_key={_apiKey}&append_to_response=credits";

                // Usamos GetAsync en lugar de GetFromJsonAsync para tener control total
                var response = await _httpClient.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // Loguea el error pero no rompas la ejecución
                    Console.WriteLine($"[Advertencia] La película con ID {movieId} no se encontró (404).");
                    return null;
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TmdbMovieResponseDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Error al obtener detalles del ID {movieId}: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<TmdbMovieDto>> GetMoviesBySearch(string search)
        {
            var url = $"{_baseUrl}search/movie?api_key={_apiKey}&query={Uri.EscapeDataString(search)}";
            var response = await _httpClient.GetFromJsonAsync<TmdbResponseDto>(url);
            return response?.Results ?? new List<TmdbMovieDto>();
        }

        public async Task<IEnumerable<TmdbGenreDto>> GetGenres()
        {
            var url = $"{_baseUrl}genre/movie/list?api_key={_apiKey}";
            var response = await _httpClient.GetFromJsonAsync<TmdbGenreResponseDto>(url);
            return response?.Genres ?? new List<TmdbGenreDto>();
        }
    }
}
