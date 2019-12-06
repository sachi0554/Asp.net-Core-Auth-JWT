using App.Core.Contract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auth20_V1.customhandler
{
    public class CustomHandler : IAuthorizationHandler
    {
        private readonly IDistributedCache _distributedCache;
        public CustomHandler(IDistributedCache  distributedCache)
        {
            _distributedCache = distributedCache;
        }
      
       

        public async  Task HandleAsync(AuthorizationHandlerContext context)
        {
            var res = new TokenRes();
            res = (TokenRes)TokenExists((AuthorizationFilterContext)context.Resource);
            if (res.IsToken)
            {
                var validd = await ValidateTokenAsync(res.Token);
                
                if (validd)
                {
                    return ;

                }
            }
            context.Fail();
            

        }



        /*  protected Task HandleAsyn(AuthorizationHandlerContext request)
          {

              var validToken = false;
              var response = new HttpResponseMessage();

              if (TokenExists(request, out string token))
              {
                  try
                  { validToken = await ValidateTokenAsync(token); } 

                  catch (Exception) { };

                  if (validToken) 
                  { response = await base.SendAsync(request, cancellationToken); }
                  else
                  { response.StatusCode = HttpStatusCode.Unauthorized; }
              }
              else
              {
                  response.StatusCode = HttpStatusCode.Unauthorized;

                  // Go in Anonymous
                  response = await base.SendAsync(request, cancellationToken);
              }

              return response;
          }*/


        private  object TokenExists(AuthorizationFilterContext request)
        {
            var tokenres = new TokenRes();
            string authHeader = request.HttpContext.Request.Headers["Authorization"];
          
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
