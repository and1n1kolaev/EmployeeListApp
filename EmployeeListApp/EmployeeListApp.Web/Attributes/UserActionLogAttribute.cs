using EmployeeListApp.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeListApp.WEB.Attributes
{
    public class UserActionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IUnitOfWork _uow;
        public UserActionAttribute(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var currentUser = context.HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetByLoginAsync(currentUser);
            if (user != null)
            {
                user.LastActivityTime = DateTime.Now; 
                await _uow.UserRepository.UpdateAsync(user);
            }
        }

     
    }
}
