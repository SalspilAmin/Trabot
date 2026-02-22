using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Helpers.DTO;
using Tradify.Data.Helpers.Results;

namespace Tradify.Service.AbstractsServices.AuthorizationServices
{
    public interface IAuthorizationService
    {

        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<Role> GetRoleById(int id);
        public  Task<List<Role>> GetRolesList();
        public  Task<string> DeleteRoleAsync(int roleId);
        public Task<ManageUserRolesResult> ManageUserRolesData(User user);
        public Task<string> UpdateUserRoles(UpdateUserRolesRequest request);

    }
}