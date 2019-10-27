using AtenticationChallenger.Business;
using AtenticationChallenger.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AutenticationOauth.Business
{
    public class Autentication : IAutentication
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public Autentication(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public string CreateSession(Login login)
        {
            var result = _signInManager.PasswordSignInAsync(login.UserName,
            login.Password,true, lockoutOnFailure:true);

            if(result.Result.Succeeded)
            {
                return CreateToken(login);
            }

            throw new ArgumentException("Usuario no valido");
        }

        private string CreateToken(Login login)
        {
            byte[] key;

            if (login == null)
            {
                throw new ArgumentException($"No existe usuario");
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
