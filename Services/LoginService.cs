using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using backend_dockerAPI.Models;
using backend_web_api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace backend_dockerAPI.Services
{
    public class LoginService
    {
        private readonly IMongoCollection<Developer> developers;
        private readonly IMongoCollection<Company> companies;
        private readonly string key;

        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HasingIterationsCount = 10101;

        public LoginService(IMongoClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase("AdvancedAppDevelopment");
            developers = database.GetCollection<Developer>("Developers");
            companies = database.GetCollection<Company>("Companies");
            this.key = configuration.GetSection("JwtKey").ToString();
        }

        public string GetClientType(string email)
        {
            var developer = developers.Find(x => x.Email == email).FirstOrDefault();
            var company = companies.Find(x => x.Email == email).FirstOrDefault();

            if (developer != null)
            {
                return "developer";
            }

            return "company";
        }

        public Client getClient(string email)
        {
            return developers.Find(x => x.Email == email).FirstOrDefault();
        }

        public Company getCompany(string email)
        {
            return companies.Find(x => x.Email == email).FirstOrDefault();
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] _passwordHashBytes;

            int _arrayLen = (SaltByteSize + HashByteSize) + 1;

            if (hashedPassword == null)
            {
                return false;
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            byte[] src = Convert.FromBase64String(hashedPassword);

            if ((src.Length != _arrayLen) || (src[0] != 0))
            {
                return false;
            }

            byte[] _currentSaltBytes = new byte[SaltByteSize];
            Buffer.BlockCopy(src, 1, _currentSaltBytes, 0, SaltByteSize);

            byte[] _currentHashBytes = new byte[HashByteSize];
            Buffer.BlockCopy(src, SaltByteSize + 1, _currentHashBytes, 0, HashByteSize);

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, _currentSaltBytes, HasingIterationsCount))
            {
                _passwordHashBytes = bytes.GetBytes(SaltByteSize);
            }

            return AreHashesEqual(_currentHashBytes, _passwordHashBytes);

        }

        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }

        public string Authenticate(string email, string password)
        {
            var developer = developers.Find(x => x.Email == email).FirstOrDefault();
            var company = companies.Find(x => x.Email == email).FirstOrDefault();
            var isCorrectPassword = false;
            if (developer != null)
            {
                isCorrectPassword = VerifyHashedPassword(developer.Password, password);
            }
            else
            {
                isCorrectPassword = VerifyHashedPassword(company.Password, password);
            }
            if(isCorrectPassword == false) return null;

            if (developer == null && company == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Email, email),
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}