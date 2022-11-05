using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entity;

namespace API.Interfaces
{
    public interface IScheduleService
    {
        public Task SceduleEmail(AppUser user, bool newStatus, int timesFive);
        // public Task SceduleInfo(AppUser user);
    }
}