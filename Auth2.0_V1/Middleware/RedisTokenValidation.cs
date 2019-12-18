using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Auth20_V1.Middleware
{
    public class RedisTokenValidation
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _distributedCache;
        public RedisTokenValidation(RequestDelegate next , IDistributedCache distributedCache)
        {
            _next = next;
            _distributedCache = distributedCache;
        }
        public async Task Invoke(HttpContext context)
        {
            var res = new TokenRes();
            res = (TokenRes)TokenExists(context);
            if (res.IsToken)
            {
                if (!await ValidateTokenAsync(res.Token))
                {
                    context.Response.StatusCode = 401; //UnAuthorized
                    return;
                }
            }
            else
            {
                 context.Response.StatusCode = 400; //Bad Request                
                 return;
            }
            await _next.Invoke(context);

        }

        private object TokenExists(HttpContext context)
        {
            var tokenres = new TokenRes();
            string authHeader = context.Request.Headers["Authorization"];

            if (!String.IsNullOrEmpty(authHeader) && authHeader.Contains(""))
            {
                var token = authHeader.Contains("Bearer") ? authHeader.Replace("Bearer", "") : authHeader.Replace("bearer", "");
                tokenres = new TokenRes { Token = token, IsToken = true };


                return tokenres;
            }
            tokenres = new TokenRes { Token = "", IsToken = false };
            return tokenres;
        }


        private async Task<bool> ValidateTokenAsync(string accessToken)
        {
            var jwt = accessToken.Trim();
            var handler = new JwtSecurityTokenHandler();
            var readableToken = handler.CanReadToken(jwt);
            if (readableToken)
            {
                var token = handler.ReadJwtToken(jwt);
                var cachedResponse = await _distributedCache.GetStringAsync(token.Id);
                if (cachedResponse != null && cachedResponse.Length > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private class TokenRes
        {
            public string Token { get; set; }
            public bool IsToken { get; set; }
        }
    }
}
