using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Unp { get; set; }
        public string Email { get; set; }
        public string LastStatus { get; set; }
        public AppUser(string unp, string email)
        {
            Unp = unp;
            Email = email;
            LastStatus = "";
        }
    }
}