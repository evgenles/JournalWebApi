using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DJournalWebApi.Date
{
    public class AuthOptions
    {
        public const string ISSUER = "JournalWebApiAuth";
        const string KEY = "aSbdQw1R3Wsda4size128";
        public const int LIFETIME = 120;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}