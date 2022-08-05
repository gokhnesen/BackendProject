using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BackendProject.Data.Requests
{
    public class UserForLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
