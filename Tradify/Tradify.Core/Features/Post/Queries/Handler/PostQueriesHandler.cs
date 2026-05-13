using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Post.Commands.Models;
using Tradify.Core.Features.Post.Queries.Models;
using Tradify.Core.Features.Post.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Enums;
using Tradify.RealTimeService.HubServices;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Post.Queries.Handler
{
    public  class PostQueriesHandler : ResponseHandler,IRequestHandler<GetPostsOfUsersQueriy, Response<IList<GetListOfPostsResult>>>
    ,IRequestHandler<GetPostPaginationQuery,Response<PaginatedResult<GetListOfPostsResult>>>
    ,IRequestHandler<GetPostByIdQuery, Response<GetListOfPostsResult>>
        {
        #region Fields
        private readonly LocalizationService localization;

        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;
        private readonly IPostService postService;
        private readonly PostHubService postHubService;
        private readonly UserManager<Tradify.Data.Entities.Identity.User> userManager;
        private readonly IFileService fileService;
        private readonly IImageOrVideoPathService imageOrVideoPathService;

        #endregion
        #region constructor
        public PostQueriesHandler(LocalizationService localization
     , IMapper mapper,
           PostHubService postHubService, UserManager<Tradify.Data.Entities.Identity.User> userManager
            , IFileService fileService,
           IImageOrVideoPathService imageOrVideoPathService,IPostService postService) : base(localization)
        {
            this.localization = localization;

            this.mapper = mapper;
            this.currentUserService = currentUserService;
            this.postHubService = postHubService;
            this.userManager = userManager;
            this.fileService = fileService;
            this.postService = postService;
        }

        






        #endregion
        #region Methods
        public async Task<Response<IList<GetListOfPostsResult>>> Handle(GetPostsOfUsersQueriy request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId.ToString())   ;
            if(user == null)  return BadRequest<IList<GetListOfPostsResult>>(localization.Get("NotFound"));
            var postsResult = mapper.Map<IList<GetListOfPostsResult>>(user.Posts) ;

            return Success<IList<GetListOfPostsResult>>(postsResult);
            
        }

        public async Task<Response<PaginatedResult<GetListOfPostsResult>>> Handle(GetPostPaginationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var postsQuery = postService.GetTableNoTracking().Where(x => !x.IsDeleted);
                var result = await mapper.ProjectTo<GetListOfPostsResult>(postsQuery).ToPaginationlist(request.PageNumber, request.PageSize);


                return Success(result);
            }
            catch (Exception ex) { throw; }
            
        }

        public async Task<Response<GetListOfPostsResult>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            try {

                var post = await postService.GetByIdAsync(request.PostId);
            
        

                if (post == null)
                    return NotFound<GetListOfPostsResult>(localization.Get("NotFound"));
                var result =  mapper.Map< GetListOfPostsResult >(post);
                return Success(result);
            }

            catch(Exception ex) {
                throw;
            }
        }

        #endregion
    }
}
