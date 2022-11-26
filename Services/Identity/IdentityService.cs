using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Common;
using Common.Extensions;
using Core.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ViewModel;

namespace Services.Customer
{
    public class IdentityService : IIdentityService
    {
        private readonly OauthSettings oauthSettings;
        private readonly IUnitOfWork unitOfWork;
       

        public IdentityService(IUnitOfWork unitOfWork, IOptionsSnapshot<OauthSettings> oauthSettings)
        {
            this.unitOfWork = unitOfWork;
            this.oauthSettings = oauthSettings.Value;
        }


        public User GetById(long id)
        {
            return unitOfWork.IdentityRepository.GetById(id).Result;
        }

        public IdentityResponseViewModel Authenticate(IdentityRequestViewModel model)
        {
            var user = unitOfWork.IdentityRepository
                .FirstOrDefault(i => string.Equals(i.EmailAddress.ToUpper(), model.UserName.ToUpper())).Result;

            // return null if user not found
            if (user == null)
                return null;

            if (!IsPasswordHashMatch(model.Password, user.Password, user.PasswordKey))           
                return null;
            
            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user.Id);

            var response = new IdentityResponseViewModel
            {
                Token = token
            };
            return response;
        }

        private static bool IsPasswordHashMatch(string password, byte[] userPassword, byte[] userPasswordKey)
        {
            using var hmac = new HMACSHA512(userPasswordKey);
            var passwordHash = hmac.ComputeHash(password.ToByteArray());
            return passwordHash.IsEqual(userPassword);
        }

        private string GenerateJwtToken(long userId)
        {
           
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(oauthSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(oauthSettings.AccessTokenExpirationInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}