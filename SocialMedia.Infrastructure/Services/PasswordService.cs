using Microsoft.Extensions.Options;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Options;
using System.Security.Cryptography;

namespace SocialMedia.Infrastructure.Services
{
    public class PasswordService : IPasswordHasher
    {
        private readonly PasswordsOptions _Options;
        public PasswordService(IOptions<PasswordsOptions> options)
        {
            _Options = options.Value;

        }
        public bool Check(string hash, string password)
        {
            throw new NotImplementedException();
        }

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                _Options.SaltSize,
                _Options.Iterations)
                )
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_Options.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);
                return $"{_Options.Iterations}.{salt}.{key}";
            }
        }
    }
}
