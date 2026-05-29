using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Interactions.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities.Posts;
using Tradify.Data.Helpers.Results;
using Tradify.RealTimeService.HubServices;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Features.Interactions.Commands.Handler
{
    public class InteractionCommandHandler :

        ResponseHandler,

        IRequestHandler<AddInteractionWithPostCommand,Response<int?>>,

        IRequestHandler<UpdateInteractionWithPostCommand,Response<string>>,

      IRequestHandler<DeleteInteractionWithPostCommand,Response<string>>
    {
        #region Fields

        private readonly
            IInteractionWithPostService
            interactionService;

        private readonly IPostService postService;

        private readonly IMapper mapper;

        private readonly PostHubService
            postHubService;

        private readonly LocalizationService
            localization;

        #endregion

        #region Constructor

        public InteractionCommandHandler(
          IInteractionWithPostService interactionService,IPostService postService,IMapper mapper,PostHubService postHubService,LocalizationService localization)
            : base(localization)
        {
            this.interactionService =
                interactionService;

            this.postService = postService;

            this.mapper = mapper;

            this.postHubService =
                postHubService;

            this.localization = localization;
        }

        #endregion

        #region Add Interaction

        public async Task<Response<int?>>
            Handle(
                AddInteractionWithPostCommand request,
                CancellationToken cancellationToken)
        {
            try
            {
                var post =
                    await postService
                        .GetByIdAsync(
                            request.PostId);

                if (post == null)
                {
                    return NotFound<int?>(
                        localization.Get(
                            "NotFound"));
                }
                if (post.interactionWithPosts.FirstOrDefault(x => x.UserId == request.UserId) != null)
                    return BadRequest<int?>(localization.Get("IsExist"));

                var interaction =
                    mapper.Map<
                        InteractionWithPost>(
                            request);

                var result =
                    await interactionService
                        .AddAsync(interaction);

                await interactionService
                    .SaveChangesAsync();

                var notifyResult =
                    mapper.Map<
                        InteractionWithPostResult>(
                            result);

                await postHubService
                    .NotifyAddInteraction(
                        notifyResult);

                return Created<int?>(
                    result.Id);
            }
            catch (Exception ex)
            {
                return BadRequest<int?>(
                    ex.Message);
            }
        }

        #endregion

        #region Update Interaction

        public async Task<Response<string>>
            Handle(
                UpdateInteractionWithPostCommand request,
                CancellationToken cancellationToken)
        {
            try
            {
                var interaction =
                    await interactionService
                        .GetByIdAsync(
                            request.InteractionId);

                if (interaction == null)
                {
                    return NotFound<string>(
                        localization.Get(
                            "NotFound"));
                }

                interaction.InteractionBy =
                    request.InteractionBy;

                await interactionService
                    .SaveChangesAsync();

                return Success(
                    localization.Get(
                        "Success"));
            }
            catch (Exception ex)
            {
                return BadRequest<string>(
                    ex.Message);
            }
        }

        #endregion

        #region Delete Interaction

        public async Task<Response<string>>
            Handle(
                DeleteInteractionWithPostCommand request,
                CancellationToken cancellationToken)
        {
            try
            {
                var interaction =
                    await interactionService
                        .GetByIdAsync(
                            request.InteractionId);

                if (interaction == null)
                {
                    return NotFound<string>(
                        localization.Get(
                            "NotFound"));
                }

                interaction.IsDeleted = true;

                await interactionService
                    .SaveChangesAsync();

                await postHubService
                    .NotifyRemoveInteraction(
                        interaction.Id);

                return Success(
                    localization.Get(
                        "Success"));
            }
            catch (Exception ex)
            {
                return BadRequest<string>(
                    ex.Message);
            }
        }

        #endregion
    }
}
