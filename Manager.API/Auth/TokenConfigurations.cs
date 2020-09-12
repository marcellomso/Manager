using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace Manager.API.Auth
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
        public string SecurityKey
        {
            get { return null; }
            set {
                SecurityKey key;
                if (value == "")
                    using (var provider = new RSACryptoServiceProvider(2048))
                    {
                        key = new RsaSecurityKey(provider.ExportParameters(true));
                    }
                else
                    key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(value));
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            }
        }
        public SigningCredentials SigningCredentials { get; private set; }
    }
}
