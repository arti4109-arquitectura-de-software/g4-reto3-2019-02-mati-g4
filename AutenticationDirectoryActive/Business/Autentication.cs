using AtenticationChallenger.Business;
using AtenticationChallenger.Entities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AutenticationDirectoryActive.Business
{
    public class Autentication : IAutentication
    {
        public string CreateSession(Login login)
        {
            Entities.Config config = JsonConvert.DeserializeObject<Entities.Config>(login.Config);

            if(Authenticate(login.UserName,login.Password,config.Server))
            {
                return CreateToken(login);
            }

            throw new ArgumentException("Usuario no valido");
        }

        public bool Authenticate(string userName, string password, string server)
        {

            
            DirectoryEntry entry = new DirectoryEntry("LDAP://" + server, userName, password);
            try
            {
                object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + userName + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (null == result)
                    return false;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error autenticando usuario en directorio activo. " + ex.Message);
            }

            return false;
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
