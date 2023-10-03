// using CQ.ApiFilters.Core;
using dotenv.net.Utilities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;

namespace PlayerFinder.Auth.Api.Filters
{
    //internal class CQAuthenticationFilter : AuthenticationHandlerFilter
    //{
    //    protected override bool IsFormatOfTokenValid(string token)
    //    {
    //        try
    //        {
    //            var tokenHandler = new JwtSecurityTokenHandler();
    //            var validationParameters = GetValidationParameters();

    //            SecurityToken validatedToken;
    //            IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }
        
    //    private TokenValidationParameters GetValidationParameters()
    //    {
    //        var firebasePrivateKey = Environment.GetEnvironmentVariable("firebase:private-key");
    //                                return new TokenValidationParameters()
    //        {
    //            ValidateLifetime = false, // Because there is no expiration in the generated token
    //            ValidateAudience = false, // Because there is no audiance in the generated token
    //            ValidateIssuer = false,   // Because there is no issuer in the generated token
    //            ValidIssuer = "Sample",
    //            ValidAudience = "Sample",
    //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(firebasePrivateKey)) // The same key as the one that generate the token
    //        };
    //    }
    //}
}
