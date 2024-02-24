using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;
using Shared.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtGeneratorController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public JwtGeneratorController(IConfiguration configuration) => _configuration = configuration;

        [HttpGet]
        public JSonTokenResponse GetToken(string user, string password)
        {
            if(string.IsNullOrEmpty(user) && string.IsNullOrEmpty(password))
            {
                // ACA DEBERIA ESTAR LA LÓGICA O EL SERVICIO
                // QUE SE ENCARGARIA DE VALIDAR LAS CREDENCIALES DEL USUARIO
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var byteKey = Encoding.UTF8.GetBytes(_configuration.GetSection(Constans.JWT_KEY).Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new(ClaimTypes.Name, user),
                }),
                Expires = DateTime.UtcNow.AddMinutes(Constans.ADD_12H),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey),
                                                                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JSonTokenResponse
            {
                Token = tokenHandler.WriteToken(token),
                Valid = true
            };
        }
    }
}
