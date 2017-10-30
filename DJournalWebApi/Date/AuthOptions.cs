using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
