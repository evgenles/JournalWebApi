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
using Microsoft.AspNetCore.Authorization;

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

        //[Authorize("Admin")]
        [HttpPost]
        [Route("register")]
        public async Task<string> Register([FromBody] Model.ViewModel.UserViewModel data)
        {
            if (await userManager.FindByNameAsync("qwerty") == null)
            {
                Teacher qw = new Teacher() { UserName = data.login, FullName = data.name };
                var result = await userManager.CreateAsync(qw, data.password);
                if(result.Succeeded) return Helpers.JsonObj.FormJson("200", "", $"User {data.login} registred");
                else return Helpers.JsonObj.FormJson("400", "", $"Uncorrect unswer, errors: {result.Errors}");
            }
            else return Helpers.JsonObj.FormJson("400", "", $"User {data.login} already exist");
        }
    }
}