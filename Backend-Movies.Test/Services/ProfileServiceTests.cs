
using AutoMapper;
using Moq;
using Movies.Application.Interfaces;
using Movies.Application.Services;
using Movies.Domain.Entities;
using Xunit;

namespace Backend_Movies.Test.Services
{
    public class ProfileServiceTests
    {
        private readonly Mock<IProfileRepository> _profileRepositoryMock;
        private readonly Mock<IMovieService> _movieServiceMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly ProfileService _service;

        public ProfileServiceTests()
        {
            _profileRepositoryMock = new Mock<IProfileRepository>();
            _movieServiceMock = new Mock<IMovieService>();
            _userServiceMock = new Mock<IUserService>();
            _mapperMock = new Mock<IMapper>();

            _service = new ProfileService(
                _userServiceMock.Object,
                _profileRepositoryMock.Object,
                _movieServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]

        public async Task AddToMyList_ShouldThrowUnauthorized_WhenUserIsNotOwner()
        {
            // Arrange
            var profile = new Movies.Domain.Entities.Profile
            {
                Id = 1,
                UserId = 999, // distinto
                MyList = new List<Movie>()
            };

            _profileRepositoryMock.Setup(x => x.GetById(1))
                .ReturnsAsync(profile);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _service.AddToMyList(1, 10, 100)
            );
        }
    }
}
