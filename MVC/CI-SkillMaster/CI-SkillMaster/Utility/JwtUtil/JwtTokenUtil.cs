using CISkillMaster.Entities.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CI_SkillMaster.Utility.JwtUtil;

public class JwtTokenUtil
{
    private readonly IConfiguration _config;

    public JwtTokenUtil(IConfiguration config)
    {
        _config = config;
    }
    #region JWT Token Helper
    public string GenerateToken(UserInformationDTO user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:SecretKey"]!));
        var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim("id", user.Email),
            new Claim("role", user.Role.ToString())
        };
        var token = new JwtSecurityToken(
            _config["JwtConfig:Issuer"],
            _config["JwtConfig:Issuer"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: credential
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        if (token == null)
            return false;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["JwtConfig:SecretKey"]);
        var audience = _config["JwtConfig:Issuer"];

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = audience,
                ValidIssuer = audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            // return user id from JWT token if validation successful
            return true;
        }
        catch(Exception ex)
        {
            Console.WriteLine("Something went wrong during jwt authentication : " + ex.Message);
            Console.WriteLine(ex.StackTrace);
            return false;
        }
    }

    #endregion 
}
