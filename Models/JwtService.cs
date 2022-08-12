using Microsoft.IdentityModel.Tokens;
using SignUpAPI.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignUpAPI.Models
{
    public class JwtService
    {
        

        public string SecretKey { get; set; }

        public int TokenDuration { get; set; }
        private readonly IConfiguration config;
        private SignUpAPIDbContext dbContext;

        public JwtService(IConfiguration _config)
        {
            config = _config;
            this.SecretKey = config.GetSection("jwtConfig").GetSection("key").Value;
            this.TokenDuration = Int32.Parse(config.GetSection("jwtConfig").GetSection("Duration").Value);

        }

        public JwtService()
        {
        }

        public JwtService(SignUpAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string GenerateToken(string id, string firstname, string lastname, string email, string employeeinvestment, string employerinvestment, string retirementinvestment)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));

            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var payload = new[]
            {
                new Claim("id", id),
                new Claim("firstname", firstname),
                new Claim("lastname", lastname),
                new Claim("email", email),
                new Claim("employeeinvestment", employeeinvestment),
                new Claim("employerinvestment", employerinvestment),
                new Claim("retirementinvestment", retirementinvestment)

            };

            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: payload,
                expires: DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials:signature
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);

            
        }

        

        /*internal object? GenerateToken(string v, string firstName, string lastName, string email)
        {
            throw new NotImplementedException();
        }

        internal object? GenerateToken(string id, string firstName, string lastName, string email)
        {
            throw new NotImplementedException();
        }*/
    }
}
