using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Posts;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class PostService : Service<Post>, IPostService
    {
        private readonly UserManager<Tradify.Data.Entities.Identity.User> _userManager; 
        public PostService(IGenericRepository<Post> repository, UserManager<Tradify.Data.Entities.Identity.User> userManager) : base(repository)
        {
            _userManager = userManager;
        }

        public async Task<List<Post>?> GetPostsOfUsers(int userId)
        {
            //get User
            var User = await _userManager.FindByIdAsync(userId.ToString());

            // check User
            if(User==null) return null;

            return User.Posts.ToList(); 
             
        }

       
    }
}
