using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;
using System.Linq;
using DTO.Enums;
using API.Filters;

namespace API.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class CheckJwtTokenAttribute : Attribute, IFilterFactory
    {
        public AccountType[] Allows { get; set; }

        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            try
            {
                CheckJwtTokenFilter filter = (CheckJwtTokenFilter)serviceProvider.GetService(typeof(CheckJwtTokenFilter));

                if (Allows != null)
                {
                    filter.Allows = Allows.ToList();
                }

                return filter;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}