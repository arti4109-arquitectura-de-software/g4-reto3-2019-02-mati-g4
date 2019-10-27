using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtenticationChallenger.Business;
using AtenticationChallenger.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutenticationLDAP.Controllers
{
    [Route("Autentication")]
    [ApiController]
    public class AutenticationController : ControllerBase
    {
        public IAutentication _autentication;

        public AutenticationController(IAutentication autentication)
        {
            _autentication = autentication;
        }

        [HttpPost]
        public async Task<string> CreateSesion(Login login)
        {
            return await Task.Run(() => _autentication.CreateSession(login));
        }
    }
}