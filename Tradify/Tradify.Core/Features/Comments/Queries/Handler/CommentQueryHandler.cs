using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Comments.Queries.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Helpers.Results;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Comments.Queries.Handler
{
    public class CommentQueryHandler : ResponseHandler,
    IRequestHandler<GetCommentsByPostIdQuery,Response<List<CommentResult>>>,
IRequestHandler<GetCommentByIdQuery,Response<CommentResult>>,
 IRequestHandler< GetRepliesByCommentIdQuery, Response<List<ReplyCommentResult>>>, IRequestHandler<GetReplyByIdQuery, Response<ReplyCommentResult>>
    {
        private readonly IReplyOFCommentService replyService;
        private readonly ICommentService commentService;
        private readonly LocalizationService localizationService;
        private readonly IMapper mapper;

        public CommentQueryHandler(
            IReplyOFCommentService replyService,
            IMapper mapper,
            LocalizationService localization,ICommentService commentService)
            : base(localization)
        {
            this.replyService = replyService;
            this.mapper = mapper;
            this.localizationService = localization;
            this.commentService = commentService;
        }
        

        public async Task<Response<List<ReplyCommentResult>>> Handle(GetRepliesByCommentIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var replies =
                    await replyService
                        .GetRepliesByCommentIdAsync(
                            request.CommentId);

                if (replies == null ||
                    !replies.Any())
                {
                    return NotFound<
                        List<ReplyCommentResult>>(
                        localizationService.Get(
                            "NotFound"));
                }

                var result =
                    mapper.Map<
                        List<ReplyCommentResult>>(
                            replies);

                return Success(result);
            }
            catch (Exception ex)
            {
                return BadRequest<
                    List<ReplyCommentResult>>(
                    ex.Message);
            }
        }

        public async Task<Response<List<CommentResult>>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var comments =
                    await commentService
                        .GetCommentsByPostIdAsync(
                            request.PostId);

                if (comments == null ||
                    !comments.Any())
                {
                    return NotFound<
                        List<CommentResult>>(
                        localizationService.Get(
                            "NotFound"));
                }

                var result =
                    mapper.Map<
                        List<CommentResult>>(
                            comments);

                return Success(result);
            }
            catch (Exception ex)
            {
                return BadRequest<
                    List<CommentResult>>(
                    ex.Message);
            }
        }

        public async Task<Response<CommentResult>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var comment =
                    await commentService
                        .GetByIdAsync(
                            request.CommentId);

                if (comment == null)
                {
                    return NotFound<
                        CommentResult>(
                        localizationService.Get(
                            "NotFound"));
                }

                var result =
                    mapper.Map<CommentResult>(
                        comment);

                return Success(result);
            }
            catch (Exception ex)
            {
                return BadRequest<
                    CommentResult>(
                    ex.Message);
            }
        }

        public async Task<Response<ReplyCommentResult>> Handle(GetReplyByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reply =
                    await replyService
                        .GetByIdAsync(
                            request.ReplyId);

                if (reply == null)
                {
                    return NotFound<
                        ReplyCommentResult>(
                        localizationService.Get(
                            "NotFound"));
                }

                var result =
                    mapper.Map<
                        ReplyCommentResult>(
                            reply);

                return Success(result);
            }
            catch (Exception ex)
            {
                return BadRequest<
                    ReplyCommentResult>(
                    ex.Message);
            }
        }
    }
}

