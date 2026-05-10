using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Post.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities.Posts;
using Tradify.Data.Enums;
using Tradify.Data.Helpers.Results;
using Tradify.RealTimeService.HubServices;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Post.Commands.Handler
{
    public class PostCommandHandler :  ResponseHandler,IRequestHandler<AddPostModelCommand,Response<int?>>
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
        public PostCommandHandler(LocalizationService localization
     , IMapper mapper,
           PostHubService postHubService, UserManager<Tradify.Data.Entities.Identity.User> userManager
            ,IFileService fileService,
           IImageOrVideoPathService imageOrVideoPathService,IPostService postService) : base(localization)
        {
            this.localization = localization;
   
            this.mapper = mapper;
            this.currentUserService = currentUserService;
            this.postHubService = postHubService;
            this.userManager = userManager; 
            this.fileService = fileService;
            this.postService = postService;
            this.imageOrVideoPathService = imageOrVideoPathService;
        }

      
        #endregion
        #region Methods
        public async Task<Response<int?>> Handle(AddPostModelCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await postService.BeginTransactionAsync())
            {

                try
                {
                    PostType postType = request.MediaFilles.Count > 0 ? PostType.ImageOrVideo : PostType.Text;
                    var post = mapper.Map<Tradify.Data.Entities.Posts.Post>(request);
                    post.PostType = (request.MediaFilles.Count==0)? PostType.Text:PostType.ImageOrVideo;
                    if (post == null) return BadRequest<int?>(localization.Get("NotFound"));
                    var user = await userManager.FindByIdAsync(request.UserId.ToString());
                    if (user == null) return BadRequest<int?>(localization.Get("NotFound"));
                    List<string> mediaErrors = new();
                    List<string> UrlsOfFilles =new();
                    foreach (var file in request.MediaFilles)
                    {
                        (string Error, string ? Url, string ? PublicId) = await fileService.UploadImageAsync(file, $"{UploadFolder.Products}");
                        switch (Error)
                        {
                            case "NoFile":
                                mediaErrors.Add(localization.Get("ImageNotFound"));
                                break;

                            case "InvalidImageType":
                                mediaErrors.Add(localization.Get("Allowedextensions"));
                                break;

                            case "FileTooLarge":
                                mediaErrors.Add(localization.Get("MaxSizeIs5MB"));
                                break;

                            case "InvalidFileType":
                                mediaErrors.Add(localization.Get("Allowedextensions"));
                                break;

                            case "FailedToUploadImage":
                                mediaErrors.Add(localization.Get("FailedToUploadImage"));
                                break;

                            case "Success":
                                {
                                    UrlsOfFilles.Add(Url);


                                }
                                break;
                        }
                        
                       

                    }
                    if (mediaErrors.Any())
                    {
                        foreach (var url in UrlsOfFilles)
                        {
                            await fileService.DeleteImageAsync(url);
                        }
                        return BadRequest<int?>(string.Join(", ", mediaErrors));
                    }
                    var resultOfAddPost = await postService.AddAsync(post);
                    if (resultOfAddPost == null) return BadRequest<int?>(localization.Get("UnprocessableEntity"));
                    await  postService.SaveChangesAsync();
                    post.ImageOrVideo_Paths = new List<ImageOrVideoPath>();
                    foreach (var UrlFile in UrlsOfFilles)
                    {
                        var imageORVideoPath = new Data.Entities.Posts.ImageOrVideoPath()
                        {
                            PostId = post.Id
                        ,
                        Posts = post
                        ,
                            Image_Or_VideoPath = UrlFile
                        };
                        var resultOfCreatImagepath = await imageOrVideoPathService.AddAsync(imageORVideoPath);
                        post.ImageOrVideo_Paths.Add(resultOfCreatImagepath);
                    }
                    if (user.Posts == null) user.Posts = new List<Tradify.Data.Entities.Posts.Post>(); // التأمين ضد الـ Null

                    user.Posts.Add(post);
                    user.Posts.Add(post);
                    var notifyPost =  mapper.Map<PostResult>(post);
                   await postHubService.NotifyAllAboutPost(notifyPost);
                    await postService.SaveChangesAsync();
                  await  userManager.UpdateAsync(user);
                    await transaction.CommitAsync();
                    return Created<int?>(post.Id,"Post created successfully");

                }
                catch (Exception ex)
                {
                 
                    transaction.RollbackAsync();
                    return BadRequest<int?>(ex.Message);
                }
            }
        }

        #endregion
    }
}
