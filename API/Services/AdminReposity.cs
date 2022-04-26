using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Helpers;
using API.Models;

namespace API.Services
{
    public interface IAdminReposity
    {
        AdminAuthenticateResponse Login(AuthenticateRequest model);
    }
    public class AdminReposity : IAdminReposity
    {
        private readonly AppSettings _appSettings;
        private readonly minidressContext _context;
        public AdminReposity(IOptions<AppSettings> appSettings, minidressContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public AdminAuthenticateResponse Login(AuthenticateRequest model)
        {
            var user = _context.Admins.SingleOrDefault(x => x.Email.Trim() == model.Email.Trim() && x.Password.Trim() == model.Password.Trim());

            // return null if user not found
            if (user == null) return null;
            string role = "";
            var _role = _context.AdminRoles.Where(x => x.AdminId == user.Id).ToList();
            var _number = 1;
            foreach (var item in _role)
            {
                role += ""+ item.RoleName.ToString().Trim();
                if(_role.Count() > _number) role += ",";
                _number++;
            }
            Admin objuser = new Admin()
            {
                Id = user.Id,
                HashId = user.HashId.Trim(),
                Name = user.Name.Trim(),
                Email = user.Email.Trim(),
                Role = role
            };

            // authentication successful so generate jwt token
            var token = generateJwtToken(objuser);

            return new AdminAuthenticateResponse(objuser, token);
        }
        private string generateJwtToken(Admin user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            ClaimsIdentity getClaimsIdentity()
            {
                return new ClaimsIdentity(
                    getClaims()
                    );

                Claim[] getClaims()
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Email, user.Email.ToString().Trim()));
                    var role = _context.AdminRoles.Where(x => x.AdminId == user.Id).ToList();
                    foreach (var item in role)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.RoleName.ToString().Trim()));
                    }
                    claims.Add(new Claim(ClaimTypes.Name, user.Name.ToString().Trim()));
                    claims.Add(new Claim(ClaimTypes.Hash, user.HashId.ToString().Trim()));
                    return claims.ToArray();
                }

            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = getClaimsIdentity(),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };



            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //         new Claim("email", user.Email.ToString().Trim()),
            //         new Claim("name", user.Name.ToString().Trim()),
            //        new Claim("role", user.Role.ToString().Trim()),
            //        new Claim("hashid", user.HashId.ToString().Trim())
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
