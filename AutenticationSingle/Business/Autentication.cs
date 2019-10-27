using AtenticationChallenger.Business;
using AtenticationChallenger.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AutenticationSingle.Business
{
    public class Autentication : IAutentication
    {
        public string CreateSession(Login login)
        {
            byte[] key;

            if (login == null)
            {
                throw new Exception($"No existe usuario");
            }

            
                key = Encoding.ASCII.GetBytes("FjfUVyRotmhgNu8wFRTldP1AqhXx2TBB");
          
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim (ClaimTypes.Name ,login.UserName)
            }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}
