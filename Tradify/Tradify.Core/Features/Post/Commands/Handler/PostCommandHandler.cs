using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        ,IRequestHandler<UpdatePostCommand,Response<int>>,IRequestHandler<DeletePostCommand,Response<string>> 
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
            request.MediaFilles ??= new List<IFormFile>();
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

        public async Task<Response<string>> Handle(DeletePostCommand request,CancellationToken cancellationToken)
        {
            using var transaction =
                await postService.BeginTransactionAsync();

            try
            {
                var post =
                    await postService.GetPostByIdWithIncludesAsync(
                        request.PostId);

                if (post == null)
                    return NotFound<string>(
                        localization.Get("NotFound"));

                // Delete files from cloud
                if (post.ImageOrVideo_Paths != null)
                {
                    foreach (var media in post.ImageOrVideo_Paths)
                    {
                        await fileService.DeleteImageAsync(
                            media.Image_Or_VideoPath);
                    }
                }

                // Delete interactions
                if (post.interactionWithPosts != null)
                {
                    foreach (var interaction in post.interactionWithPosts)
                    {
                        interaction.IsDeleted = true;
                    }
                }

                // Delete comments
                if (post.Comments != null)
                {
                    foreach (var comment in post.Comments)
                    {
                        comment.IsDeleted = true;
                    }
                }

                // Delete media records
                if (post.ImageOrVideo_Paths != null)
                {
                    foreach (var media in post.ImageOrVideo_Paths)
                    {
                        media.IsDeleted = true;
                    }
                }

                // Delete post
                post.IsDeleted = true;

                await postService.SaveChangesAsync();

                await transaction.CommitAsync();

                return Success("Post deleted successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest<string>(ex.Message);
            }
        }

        public async Task<Response<int>> Handle(UpdatePostCommand request,CancellationToken cancellationToken)
        {
            using var transaction =
                await postService.BeginTransactionAsync();

            try
            {
                var post =
                    await postService
                    .GetPostByIdWithIncludesAsync(request.PostId);

                if (post == null)
                    return NotFound<int>(
                        localization.Get("NotFound"));

                // Validation
                if (request.NewMediaFiles?.Any() == true &&
                    !request.ReplaceMedia)
                {
                    return BadRequest<int>(
                        "ReplaceMedia must be true when uploading media.");
                }

                // Update text fields
                post.Content = request.Content;
                post.Caption = request.Caption;
                post.IsUpdated = true;

                // Handle media replacement
                if (request.ReplaceMedia)
                {
                    // Delete old media
                    if (post.ImageOrVideo_Paths != null)
                    {
                        foreach (var media in post.ImageOrVideo_Paths
                                     .Where(x => !x.IsDeleted))
                        {
                            await fileService.DeleteImageAsync(
                                media.Image_Or_VideoPath);

                            media.IsDeleted = true;
                            media.IsUpdated = true;
                        }
                        post.ImageOrVideo_Paths = null; 
                    }
                    

                    // Upload new files
                    if (request.NewMediaFiles != null &&
                        request.NewMediaFiles.Any())
                    {
                        List<string> uploadedUrls = new();
                        List<string> errors = new();

                        foreach (var file in request.NewMediaFiles)
                        {
                            var uploadResult =
                                await fileService.UploadImageAsync(
                                    file,
                                    UploadFolder.Post.ToString());

                            switch (uploadResult.Error)
                            {
                                case "NoFile":
                                    errors.Add(localization.Get("ImageNotFound"));
                                    break;

                                case "InvalidImageType":
                                    errors.Add(localization.Get("Allowedextensions"));
                                    break;

                                case "InvalidFileType":
                                    errors.Add(localization.Get("Allowedextensions"));
                                    break;

                                case "FileTooLarge":
                                    errors.Add(localization.Get("MaxSizeIs5MB"));
                                    break;

                                case "FailedToUploadImage":
                                    errors.Add(localization.Get("FailedToUploadImage"));
                                    break;

                                case "Success":
                                    uploadedUrls.Add(uploadResult.Url);
                                    break;
                            }
                        }

                        if (errors.Any())
                        {
                            foreach (var url in uploadedUrls)
                            {
                                await fileService.DeleteImageAsync(url);
                            }

                            await transaction.RollbackAsync();

                            return BadRequest<int>(
                                string.Join(", ", errors));
                        }

                        foreach (var url in uploadedUrls)
                        {
                            var media = new ImageOrVideoPath
                            {
                                PostId = post.Id,
                                Posts = post,
                                Image_Or_VideoPath = url
                            };

                            await imageOrVideoPathService.AddAsync(media);
                        }

                        post.PostType = PostType.ImageOrVideo;
                    }
                    else
                    {
                        // User removed all media
                        post.PostType = PostType.Text;
                    }
                }

                // Prevent empty post
                bool hasMedia =
                    post.ImageOrVideo_Paths != null &&
                    post.ImageOrVideo_Paths.Any(x => !x.IsDeleted);

                bool hasText =
                    !string.IsNullOrWhiteSpace(post.Content) ||
                    !string.IsNullOrWhiteSpace(post.Caption);

                if (!hasText && !hasMedia)
                {
                    return BadRequest<int>(
                        "Post cannot be empty.");
                }

                await postService.SaveChangesAsync();

                // Optional SignalR Notification
                // var notifyPost = mapper.Map<PostResult>(post);
                // await postHubService.NotifyPostUpdated(notifyPost);

                await transaction.CommitAsync();

                return Success(post.Id);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return BadRequest<int>(ex.Message);
            }
        }
    }
        #endregion
}

