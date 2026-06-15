using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Posts;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class PostService : Service<Post>, IPostService
    {
        private readonly UserManager<Tradify.Data.Entities.Identity.User> _userManager; 
        private  readonly ApplicationDbContext context;
        public PostService(IGenericRepository<Post> repository, UserManager<Tradify.Data.Entities.Identity.User> userManager,ApplicationDbContext context) : base(repository)
        {
            _userManager = userManager;
            this.context = context;     
        }

        public async Task<List<Post>?> GetPostsOfUsers(int userId)
        {
            //get User
            var User = await _userManager.FindByIdAsync(userId.ToString());

            // check User
            if(User==null) return null;

            return User.Posts.ToList(); 
             
        }
        public async Task<Post?> GetPostByIdWithIncludesAsync(int postId)
        {
            return await context.Posts
                .Include(x => x.ImageOrVideo_Paths)
                .Include(x => x.interactionWithPosts)
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == postId && !x.IsDeleted);
        }

    }
}
