using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Comments.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities.Comments;
using Tradify.Data.Helpers.Results;
using Tradify.RealTimeService.HubServices;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Features.Comments.Commands.Handler
{
    public class CommentCommandHandler :
     ResponseHandler,
     IRequestHandler<AddCommentCommand, Response<int?>>,
            IRequestHandler<UpdateCommentCommand, Response<string>>,
    IRequestHandler<DeleteCommentCommand, Response<string>>,
         IRequestHandler<
        AddReplyCommentCommand,
        Response<int?>>,

    IRequestHandler<
        UpdateReplyCommentCommand,
        Response<string>>,

    IRequestHandler<
        DeleteReplyCommentCommand,
        Response<string>>

    {
        private readonly ICommentService commentService;
        private readonly IMapper mapper;
        private readonly PostHubService postHubService;
        private readonly LocalizationService localizationService;
        private readonly IReplyOFCommentService replyOFCommentService;
        private readonly IPostService postService;  

        public CommentCommandHandler(
            ICommentService commentService,
            IMapper mapper,
            PostHubService postHubService,
            LocalizationService localization,
            IReplyOFCommentService replyOFCommentService,IPostService postService) : base(localization)
        {
            this.commentService = commentService;
            this.mapper = mapper;
            this.postHubService = postHubService;
            this.localizationService = localization;
            this.replyOFCommentService = replyOFCommentService;
            this.postService = postService;
        }

        public async Task<Response<int?>> Handle(
            AddCommentCommand request,
            CancellationToken cancellationToken)
        {
            
            try
            {
                var comment = mapper.Map<Comment>(request);
                var  post = await postService.GetByIdAsync(request.PostId); 
                var result = await commentService.AddAsync(comment);
                post.Comments.Add(comment);

                await commentService.SaveChangesAsync();

                var notifyComment =
                    mapper.Map<CommentResult>(result);

                await postHubService
                    .NotifyAddComment(notifyComment);

                return Created<int?>(result.Id);
            }
            catch (Exception ex)
            {
                return BadRequest<int?>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(
       UpdateCommentCommand request,
       CancellationToken cancellationToken)
        {
            try
            {
                var comment =
                    await commentService.GetByIdAsync(
                        request.CommentId);

                if (comment == null)
                    return NotFound<string>(localizationService.Get("NotFound"));

                comment.Content = request.Content;
                comment.IsUpdated = true;

                await commentService.SaveChangesAsync();

                return Success("Success");
            }
            catch (Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }
        }


        public async Task<Response<string>> Handle(
       DeleteCommentCommand request,
       CancellationToken cancellationToken)
        {
            try
            {
                var comment =
                    await commentService.GetByIdAsync(
                        request.CommentId);
                var post = await postService.GetByIdAsync(request.PostId);
                if (comment == null || post ==null)
                    return NotFound<string>(localizationService.Get("NotFound"));

                comment.IsDeleted = true;

             
                post.Comments.Remove(comment);
                await commentService.SaveChangesAsync();

                await postHubService
                    .NotifyDeleteComment(comment.Id);

                return Success(localizationService.Get("Success"));
            }
            catch (Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(DeleteReplyCommentCommand request, CancellationToken cancellationToken)
        {


            try
            {
                var reply =
                    await replyOFCommentService
                        .GetByIdAsync(
                            request.ReplayCommentId);
                var comment = await commentService.GetByIdAsync(request.CommentId);
                var post = await postService.GetByIdAsync(request.PostId);

                if (reply == null || comment == null || post == null)
                    return NotFound<string>(
                        localizationService.Get("NotFound"));
                reply.IsDeleted = true;
                comment.ReplyOFComments.Remove(reply);
                    

                await replyOFCommentService
                    .SaveChangesAsync();

                return Success(
                    localizationService.Get("Success"));
            }
            catch (Exception ex)
            {
                return BadRequest<string>(
                    ex.Message);
            }

        }

        public async Task<Response<string>> Handle(UpdateReplyCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var reply =
                    await replyOFCommentService
                        .GetByIdAsync(
                            request.ReplyId);

                if (reply == null)
                    return NotFound<string>(localizationService.Get("NotFound"));

                reply.Content = request.Content;

                

                await replyOFCommentService
                    .SaveChangesAsync();

                return Success(localizationService.Get("Success"));
            }
            catch (Exception ex)
            {
                return BadRequest<string>(
                    ex.Message);
            }
        }

        public async Task<Response<int?>> Handle(AddReplyCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var reply =
                    mapper.Map<ReplyOFComment>(
                        request);

                var  comment = await commentService.GetByIdAsync(request.CommentId);
                if(comment == null) return NotFound<int?>(
                        localizationService.Get("NotFound"));
                var result =
                    await replyOFCommentService.AddAsync(
                        reply);
                comment.ReplyOFComments.Add(reply);

                await replyOFCommentService.SaveChangesAsync();

                var notifyReply =
                    mapper.Map<ReplyCommentResult>(
                        result);

                await postHubService
                    .NotifyAddReply(
                        notifyReply);

                return Created<int?>(
                    result.Id);
            }
            catch (Exception ex)
            {
                return BadRequest<int?>(
                    ex.Message);
            }
        }
    }
}
