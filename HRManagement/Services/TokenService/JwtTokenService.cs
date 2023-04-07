using HRManagement.Models;
using HRManagement.ValidateData;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRManagement.Services.TokenService
{
    public class JwtTokenService
    {
        private static IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }
        public Task<string> GenerateJwtAccessToken(UserModel userInfo, IList<string> userRoles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName)
            };
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var issuer = _config["JWT:Issuer"] ?? throw new ArgumentNullException("Missing configuration for JWT:Issuer");
            var audience = _config["JWT:Audience"] ?? throw new ArgumentNullException("Missing configuration for JWT:Audience");
            var secretKey = _config["JWT:SecretKey"] ?? throw new ArgumentNullException("Missing configuration for JWT:SecretKey");

            var token = GenerateJwtToken(issuer, audience, secretKey, 10, claims);

            return token;
        }


        private static Task<string> GenerateJwtToken(string issuer, string audience, string secretKey,
            double expirationTimeInMinutes, IEnumerable<Claim>? claims = null)
        {
            ValidateOnNull<string>.ValidateDataOnNull(secretKey);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer, audience, claims,
                expires: DateTime.Now.AddMinutes(expirationTimeInMinutes),
                signingCredentials: credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
        public Task<string> GenerateJwtRefreshToken()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var issuer = _config["JWT:Issuer"] ?? throw new ArgumentNullException("Missing configuration for JWT:Issuer");
            var audience = _config["JWT:Audience"] ?? throw new ArgumentNullException("Missing configuration for JWT:Audience");
            var secretKey = _config["JWT:SecretKey"] ?? throw new ArgumentNullException("Missing configuration for JWT:SecretKey");

            var token = GenerateJwtToken(issuer, audience, secretKey, 262800, claims);

            return token;
        }

        public Task<bool> ValidateRefreshToken(string refreshToken)
        {
            ValidateOnNullAndEmpty<string>.ValidateDataOnNullAndEmpty(refreshToken);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var issuer = _config["JWT:Issuer"] ?? throw new ArgumentNullException("Missing configuration for JWT:Issuer");
            var audience = _config["JWT:Audience"] ?? throw new ArgumentNullException("Missing configuration for JWT:Audience");
            var secretKey = _config["JWT:SecretKey"] ?? throw new ArgumentNullException("Missing configuration for JWT:SecretKey");

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            _ = tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validToken);

            return Task.FromResult(true);
        }

    }
}
