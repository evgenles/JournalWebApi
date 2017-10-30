using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using DJournalWebApi.Date;
using DJournalWebApi.Model;
using DJournalWebApi.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DJournalWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : ApiController
    {
        private readonly SignInManager<Teacher> _signManager;
        private readonly UserManager<Teacher> _userManager;

        public AccountController(UserManager<Teacher> userManager, SignInManager<Teacher> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserViewModel data)
        {
            var identity = await GetIdentity(data.login, data.password);
            if (identity == null) return Json(401, "", "Invalid username or password.");

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler()
                .WriteToken(jwt);

            // сериализация ответа
            return Json(data: new
            {
                token = encodedJwt
            });
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var person = await _userManager.FindByNameAsync(username);
            if (person == null) return null;

            var signInResult = await _signManager.PasswordSignInAsync(person, password, false, false);

            if (!signInResult.Succeeded) return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, person.UserName)
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;

            // если пользователя не найдено
        }

        [Authorize("Admin")]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserViewModel data)
        {
            if (await _userManager.FindByNameAsync("qwerty") != null)
                return Json(400, "", $"User {data.login} already exist");

            var qw = new Teacher {UserName = data.login};
            var result = await _userManager.CreateAsync(qw, data.password);

            return result.Succeeded
                ? Json(200, "", $"User {data.login} registred")
                : Json(400, "", $"Incorrect answer, errors: {result.Errors}");
        }
    }
}