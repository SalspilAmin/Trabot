using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Helpers.DTO;
using Tradify.Infrastructure.Context;
using Tradify.Service.AbstractsServices.AuthorizationServices;
//using IAuthorizationService = Tradify.Service.AbstractsServices.AuthorizationServices.IAuthorizationService;

namespace Tradify.Service.Services.AuthorizationServices
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;
        #endregion
        #region Constructors
        public AuthorizationService(RoleManager<Role> roleManager,
                                    UserManager<User> userManager,
                                    ApplicationDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }


        #endregion
        #region handle Functions
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return "Success";
            return "Failed";
        }



        public async Task<bool> IsRoleExistByName(string roleName)
        {
            //var role=await _roleManager.FindByNameAsync(roleName);
            //if(role == null) return false;
            //return true;
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            //check role is exist or not
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
                return "notFound";
            role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return "Success";
            var errors = string.Join("-", result.Errors);
            return errors
                ;
        }
        public async Task<Role> GetRoleById(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<List<Role>> GetRolesList()
        {
            return await _roleManager.Roles.ToListAsync();
        }
        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return "NotFound";
            //Chech if user has this role or not
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            //return exception 
            if (users != null && users.Count() > 0) return "Used";
            //delete
            var result = await _roleManager.DeleteAsync(role);
            //success
            if (result.Succeeded) return "Success";
            //problem
            var errors = string.Join("-", result.Errors);
            return errors;
        }
        #endregion
    }
}
