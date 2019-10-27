using AtenticationChallenger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtenticationChallenger.Business
{
    public interface IAutentication
    {
        string CreateSession(Login login);
    }
}
