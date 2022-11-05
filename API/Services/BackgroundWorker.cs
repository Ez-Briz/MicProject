using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;

namespace API.Services
{
    public class BackgroundWorker : IHostedService, IDisposable
    {
        private readonly ILogger<BackgroundWorker> _logger;
        private Timer _timer;
        private readonly IInfoService _infoService;
        private readonly IUserService _userService;
        private readonly IScheduleService _scheduleService;


        public BackgroundWorker(ILogger<BackgroundWorker> logger, IServiceScopeFactory factory)
        {
            _logger = logger;
            using var scope = factory.CreateScope();
            _infoService = scope.ServiceProvider.GetService<IInfoService>();
            _userService = scope.ServiceProvider.GetService<IUserService>();
            _scheduleService = scope.ServiceProvider.GetService<IScheduleService>();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
            
            var tomorrow = new DateTime(2022, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 6, 0, 0);
            if (DateTime.Now.Hour < 6) tomorrow.AddDays(-1);
            var diff = tomorrow - DateTime.Now;
            _timer = new Timer(DoWork, null, diff, 
                TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            var users = await _userService.GetAllUsers();
            for(int i=0; i <= users.Count; i++)
            {
                var timesFive = i / 100;
                var isNewStatus = await _infoService.FullCheck(users[i].Unp);
                await _scheduleService.SceduleEmail(users[i], isNewStatus, timesFive);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}