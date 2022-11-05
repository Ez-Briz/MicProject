using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entity;
using API.Interfaces;
using Hangfire;

namespace API.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IEmailService _emailService;

        public ScheduleService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public Task SceduleEmail(AppUser user, bool newStatus, int timesFive)
        {
            var message = newStatus ? "New" : "Old";
            var sevenOClock = new DateTime(2022, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, timesFive*5, 0);
            if (DateTime.Now.Hour < 6) sevenOClock.AddDays(-1);
            Console.WriteLine(sevenOClock);
            BackgroundJob.Schedule(() => _emailService.SendMessage(user.Email, message, "Daily message"), sevenOClock);
            return Task.CompletedTask;
        }

        // public Task SceduleInfo(AppUser user)
        // {
        //     var sixOClock = new DateTime(2022, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 6, 0, 0);
        //     if (DateTime.Now.Hour < 6) sixOClock.AddDays(-1);
        //     Console.WriteLine(sixOClock);

        //     BackgroundJob.Schedule(() => _infoService.FullCheck(user.Unp), sixOClock);
        //     return Task.CompletedTask;
        // }
    }
}