using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using DTO.Enums;
using API.Utils;
using Service.Interfaces.Unit;
using Service.Models;

namespace API.Filters
{
    public class CheckJwtTokenFilter : IAuthorizationFilter
    {
        private readonly IServiceUnit _service;

        public List<AccountType> Allows { get; set; }

        public CheckJwtTokenFilter(IServiceUnit service)
        {
            _service = service;
            Allows = new List<AccountType>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                string authHeaderValue = string.Empty;

                // Check for header
                if (context.HttpContext.Request.Headers.Keys.Any(a => a.Equals(ApiHeaders.AUTHORIZATION, StringComparison.CurrentCultureIgnoreCase)))
                {
                    authHeaderValue = context.HttpContext.Request.Headers[ApiHeaders.AUTHORIZATION];

                    // Check for header value
                    if (string.IsNullOrWhiteSpace(authHeaderValue))
                    {
                        context.Result = new BadRequestObjectResult(ServiceResults.Errors.HeaderValueMissing<object>(ApiHeaders.AUTHORIZATION, null));

                        return;
                    }

                }
                else
                {
                    context.Result = new BadRequestObjectResult(ServiceResults.Errors.HeaderMissing<object>(ApiHeaders.AUTHORIZATION, null));

                    return;
                }

                //TODO: Check jwt token
                //if (!_service.Token.IsTokenValid(authHeaderValue, Allows))
                //{
                //    context.Result = new BadRequestObjectResult(ServiceResults.Errors.InvalidJwtToken<object>(null));
                //}
            }
            catch (Exception ex)
            {
                context.Result = new BadRequestObjectResult(ServiceResults.Errors.UnhandledError<object>(ex.ToString(), null));
            }
        }
    }
}