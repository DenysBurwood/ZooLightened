using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zoo.DL.Entities.Humans;

namespace Zoo.API.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config=config;
        }

        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>(){
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.IsEmployee.ToString()),
                };

            //  Creadantial pour signer le token (clé + algorithme)
            string secretKey = _config["Jwt:Key"];
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)); //  Premier niveau de cryptage
            SigningCredentials creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);      //  Deuxième niveau de cryptage

            //  Géération du token
            JwtSecurityToken token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
