using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entity;
using API.Interfaces;
using Newtonsoft.Json;

namespace API.Services
{
    public class InfoService : IInfoService
    {
        private readonly IUserService _userService;

        public InfoService(IUserService userService)
        {
            _userService = userService;
        }

        public Task<bool> CheckStatus(AppUser user, string newStatus)
        {
            var result = user.LastStatus != newStatus;
            if (result)
                user.LastStatus = newStatus;
            return Task.FromResult(result);
        }

        public async Task<Info> GetInfoFromPortal(string unp)
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("http://www.portal.nalog.gov.by/grp/")
            };
            string request = $"getData?unp={unp}&charset=UTF-8&type=json";
            var response = await client.GetAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseMessage = await response.Content.ReadAsStringAsync();
            var info = JsonConvert.DeserializeObject<Info>(responseMessage);
            return info;
        }

        public async Task<bool> FullCheck(string unp)
        {
            var user = await _userService.GetUserByUnpAsync(unp);
            var info = await GetInfoFromPortal(unp);
            return await CheckStatus(user, info.Row.Ckodsost);
            //await _sceduleService.SceduleEmail(user, user?.LastStatus != info?.Row?.Ckodsost);
            //await _sceduleService.SceduleInfo(user);
        }
    }
}