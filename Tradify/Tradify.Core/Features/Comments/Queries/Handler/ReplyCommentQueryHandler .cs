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

namespace Tradify.Core.Features.Comments.Queries.Handler
{
    public class CommentQueryHandler : ResponseHandler,
    IRequestHandler<GetRepliesByCommentIdQuery,Response<List<ReplyCommentResult>>>
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
                        .GetByIdAsync(
                            request.CommentId);

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
    }
}
