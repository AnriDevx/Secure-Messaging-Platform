using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Sync.Api.Repositories;

namespace Sync.Api.BackgroundJobs
{
    public class AccountDeletionJob : IHostedService, IDisposable
    {
        private readonly IUserRepository _userRepository;
        private Timer? _timer;

        public AccountDeletionJob(IUserRepository? userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Run every 24 hours
            _timer = new Timer(ExecuteAsync, null, TimeSpan.Zero, TimeSpan.FromHours(24));
            return Task.CompletedTask;
        }

        private async void ExecuteAsync(object? state)
        {
            var threshold = DateTime.UtcNow.AddMonths(-6);
            var inactiveUsers = await _userRepository.GetInactiveUsersAsync(threshold);
            foreach (var user in inactiveUsers)
            {
                if (user.LastActive < threshold)
                {
                    await _userRepository.DeleteAsync(user.Id.ToString());
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}