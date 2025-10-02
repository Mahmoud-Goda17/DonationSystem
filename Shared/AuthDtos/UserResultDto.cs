using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.AuthDtos
{
    public class UserResultDto
    {    
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Message{ get; set; }
        public bool IsAuthenticated { get; set; }
        public IList<string> Roles { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
