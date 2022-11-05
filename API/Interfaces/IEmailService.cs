using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> SendMessage(string email, string message, string subject);
    }
}