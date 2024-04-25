using Microsoft.AspNetCore.Http;


namespace API.Utils
{
    public static class RequestUtil
    {
        public static string GetToken(HttpContext context)
        {
            string authHeaderValue = context.Request.Headers[ApiHeaders.AUTHORIZATION];

            var token = authHeaderValue.Split(" ")[1];

            return token;
        }
    }
}
