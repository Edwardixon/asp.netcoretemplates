using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtGenerator : IJwtGenerator
{
    private readonly IConfiguration _configuration;
    public JwtGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim>() 
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
        };

        // Keep this string secret
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));
        // Generate credentials
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Generate token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7), // TODO: Should be a lot lower 
            SigningCredentials = creds
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}