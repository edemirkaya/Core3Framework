using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CommonCore.Extentions;
using CommonCore.Types.Enums;
using Core3_Framework.Business.Infrastructure.Settings;
using Core3_Framework.Contracts.DataContracts;
using Core3_Framework.Contracts.DTO;
using Core3_Framework.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace App.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/security")]
    public class JwtIssuerController : Controller
    {
        private readonly IJwtIssuerOptions JwtOptions;
        private readonly IUserService kullaniciServis;

        public JwtIssuerController(IJwtIssuerOptions jwtOptions, IUserService _kullaniciServis)
        {
            JwtOptions = jwtOptions;
            kullaniciServis = _kullaniciServis;
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginViewModel resource)
        {
            if (resource == null)
                return BadRequest("Hata");


            string hataMesaji = string.Empty;

            var sonuc = kullaniciServis.GetUser(resource.UserName, resource.Password);

            if (sonuc == null)
                return null;
            var token = await CreateJwtTokenAsync(sonuc.Result);


            var result = new ContentResult() { Content = token, ContentType = "application/text" };
            return result;

        }

        private async Task<String> CreateJwtTokenAsync(Users user)
        {
            // Create JWT claims
            var claims = new List<Claim>(new[]
            {
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, JwtOptions.Issuer),   
                
                // UserName
                new Claim(JwtRegisteredClaimNames.Sub, user.FullName),     
                
                // FirstName
                new Claim(JwtRegisteredClaimNames.GivenName, user.FullName),     

                // Email is unique
                new Claim(JwtRegisteredClaimNames.Email, user.Username),        

                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, await JwtOptions.JtiGenerator()), 

                // Issued at
                new Claim(JwtRegisteredClaimNames.Iat, JwtOptions.IssuedAt.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64),

                //Kullanici.Id // IslemYapanKullanici
                new Claim("UserId", user.Id.ToString())
            });

            // Prepare Jwt Token
            var jwt = new JwtSecurityToken(
                issuer: JwtOptions.Issuer,
                audience: JwtOptions.Audience,
                claims: claims,
                notBefore: JwtOptions.NotBefore,
                expires: JwtOptions.Expires,
                signingCredentials: JwtOptions.SigningCredentials);

            // Serialize token
            var result = new JwtSecurityTokenHandler().WriteToken(jwt);

            return result;
        }


        [HttpPost(nameof(RenewToken))]
        public async Task<IActionResult> RenewToken(String jwtToken)
        {
            // Setup handler for processing Jwt token
            var tokenHandler = new JwtSecurityTokenHandler();

            // Setup token checking
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = JwtOptions.SigningCredentials.Key,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            try
            {
                // retrieve principal from Jwt token
                var principal = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out var validatedToken);

                // cast needed to access Claims property
                var securityToken = validatedToken as JwtSecurityToken;

                var userName = securityToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value;
                var serviceResult = kullaniciServis.GetUser(userName);

                if (serviceResult.ServiceResultType == EnumServiceResultType.Error || serviceResult.Result == null)
                    return BadRequest("Invalid credentials");

                var result = await CreateJwtTokenAsync(serviceResult.Result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}