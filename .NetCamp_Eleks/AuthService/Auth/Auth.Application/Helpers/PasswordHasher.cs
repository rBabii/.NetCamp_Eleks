using Auth.Application.Result;
using Auth.Application.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Helpers
{
    public class PasswordHasher
    {
        private const int SaltSize = 16; // 128 bit 
        private const int KeySize = 32; // 256 bit
        private readonly IOptions<HashingOptions> _hashingOptions;

        public PasswordHasher(IOptions<HashingOptions> HashingOptions)
        {
            _hashingOptions = HashingOptions;
        }

        public CheckPasswordResult Check(string hash, string password)
        {
            var options = _hashingOptions.Value;

            var parts = hash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. " +
                  "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            var needsUpgrade = iterations != options.Iterations;

            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              salt,
              iterations,
              HashAlgorithmName.SHA256))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                var verified = keyToCheck.SequenceEqual(key);

                return new CheckPasswordResult(verified, needsUpgrade);
            }
        }

        public string Hash(string password)
        {
            var options = _hashingOptions.Value;

            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              SaltSize,
              options.Iterations,
              HashAlgorithmName.SHA256))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{options.Iterations}.{salt}.{key}";
            }
        }
    }
}
