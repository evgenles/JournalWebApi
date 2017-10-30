using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DJournalWebApi.Data
{
    public class AuthOptions
    {
        public const string Issuer = "JournalWebApiAuth";
        const string Key = "aSbdQw1R3Wsda4size128";
        public const int Lifetime = 120;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}