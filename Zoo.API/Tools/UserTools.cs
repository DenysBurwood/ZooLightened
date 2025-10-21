using System.Security.Claims;

namespace Zoo.API.Tools
{
    public static class UserTools
    {
        public static int GetUserID(this ClaimsPrincipal claim) 
        {
            return int.Parse(claim.FindFirst(ClaimTypes.Sid)!.Value);
        }

        public static string GetUserEmail(this ClaimsPrincipal claim) 
        {
            return claim.FindFirst(ClaimTypes.Email)!.Value;
        }

        //public static string GetRole()
    }
}
