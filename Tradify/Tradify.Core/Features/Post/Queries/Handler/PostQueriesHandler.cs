using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Post.Commands.Models;
using Tradify.Core.Features.Post.Queries.Models;
using Tradify.Core.Features.Post.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Data.Enums;
using Tradify.RealTimeService.HubServices;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Post.Queries.Handler
{
    public  class PostQueriesHandler : ResponseHandler,IRequestHandler<GetPostsOfUsersQueriy, Response<IList<GetListOfPostsResult>>>
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
           IImageOrVideoPathService imageOrVideoPathService) : base(localization)
        {
            this.localization = localization;

            this.mapper = mapper;
            this.currentUserService = currentUserService;
            this.postHubService = postHubService;
            this.userManager = userManager;
            this.fileService = fileService;
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

        #endregion
    }
}
