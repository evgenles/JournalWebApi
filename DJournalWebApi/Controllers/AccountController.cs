using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DJournalWebApi.Model;
using System.IdentityModel.Tokens.Jwt;
using DJournalWebApi.Date;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace DJournalWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<Teacher> userManager;
        private readonly SignInManager<Teacher> signManager;

        public AccountController(UserManager<Teacher> _userManager, SignInManager<Teacher> _signManager)
        {
            userManager = _userManager;
            signManager = _signManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<string> Login([FromBody] Model.ViewModel.UserViewModel data)
        {
            var identity = await GetIdentity(data.login, data.password);
            if (identity == null)
            {
                return Helpers.JsonObj.FormJson("401", "", "Invalid username or password.");
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    );
            var encodedJwt = new JwtSecurityTokenHandler()
                .WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
            };
            
            // сериализация ответа
           return Helpers.JsonObj.FormJson("200", response, "");
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            Teacher person = await userManager.FindByNameAsync(username);
            if (person != null)
            {
                var signInResult = await signManager.PasswordSignInAsync(person, password, false, false);

                if (signInResult.Succeeded)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.UserName),
                };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
            }

            // если пользователя не найдено
            return null;
        }
    }
}