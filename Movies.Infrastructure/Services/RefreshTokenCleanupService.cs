using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movies.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Movies.Infrastructure.Services;

public class RefreshTokenCleanupService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<RefreshTokenCleanupService> _logger;

    public RefreshTokenCleanupService(IServiceScopeFactory scopeFactory, ILogger<RefreshTokenCleanupService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var refreshTokenService = scope.ServiceProvider.GetRequiredService<IRefreshTokenService>();

                await refreshTokenService.DeleteExpiredTokensAsync();
                _logger.LogInformation("Expired refresh tokens cleaned up successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cleaning up expired refresh tokens");
            }

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}