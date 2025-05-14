using System.Security.Claims;

namespace BTPTPMQL.Models.ViewModels
{
    public class UserClaimVM
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public IList<Claim>? UserClaims { get; set; }
        public UserClaimVM(string userId, string userName, IList<Claim> userClaims)
        {
            UserId = userId;
            UserName = userName;
            UserClaims = userClaims;
        }
        
        
        
    }
   
}