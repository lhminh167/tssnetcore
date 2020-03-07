using Tss.Core.Interfaces;
using Microsoft.Extensions.Options;
using Tss.Core.Models;
using Ardalis.GuardClauses;
using Tss.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using Tss.Core.Extensions;

namespace Microsoft.eShopWeb.Infrastructure.Helpers
{
    public class AuthHelper : IAuthHelper
    {
        private readonly AppSettings _settings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly List<Claim> _claims;

        public AuthHelper(
            IOptions<AppSettings> settings,
            IHttpContextAccessor httpContextAccessor
        )
        {
            Guard.Against.Null(settings, nameof(settings));
            Guard.Against.Null(settings.Value, nameof(settings.Value));
            Guard.Against.Null(httpContextAccessor, nameof(httpContextAccessor));

            _settings = settings.Value;
            _httpContextAccessor = httpContextAccessor;
            _claims = _httpContextAccessor.HttpContext.User.Claims.ToList();
        }

        public string GetLoginToken(Customer customer)
        {
            Guard.Against.Null(customer, nameof(customer));

            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, customer.CustomerId.ToString()),
                new Claim(ClaimTypes.Name, customer.Name),
                new Claim(ClaimTypes.Role, customer.GetRole()),
            };

            return GetAuthToken(claims);
        }

        public ShoppingCartAuthInfo GetShoppingCartAuthInfo()
        {
            string cartId = GetShoppingCartId() ?? GenerateShoppingCartId();

            Claim[] claims = new Claim[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier, 
                    cartId
                ),
                new Claim(
                    ClaimTypes.Name, 
                    GetClaim(ClaimTypes.Name) ?? "Visitor"
                ),
                new Claim(
                    ClaimTypes.Role,
                    GetClaim(ClaimTypes.Role) ?? Role.Customer.Message
                )
            };

            return new ShoppingCartAuthInfo(cartId, GetAuthToken(claims));
        }

        public string GetRole()
        {
            return GetClaim(ClaimTypes.Role);
        }

        private string GenerateShoppingCartId()
        {
            return Guid.NewGuid().ToString();
        }

        public string GetShoppingCartId()
        {
            return GetClaim(ClaimTypes.NameIdentifier);
        }

        public int GetCustomerId()
        {
            return GetClaim(ClaimTypes.NameIdentifier).ToInt();
        }

        private string GetClaim(string type)
        {
            Guard.Against.NullOrWhiteSpace(type, nameof(type));
            return _claims.Find(c => c.Type == type)?.Value;
        }

        private string GetAuthToken(Claim[] claims)
        {
            Guard.Against.Zero(claims.Length, nameof(claims.Length));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.AuthenticationTokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}