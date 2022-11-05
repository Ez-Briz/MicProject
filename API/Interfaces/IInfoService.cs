using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entity;

namespace API.Interfaces
{
    public interface IInfoService
    {
        public Task<Info> GetInfoFromPortal(string unp);
        public Task<bool> CheckStatus(AppUser user, string newStatus);

        public Task<bool> FullCheck(string unp);
    }
}