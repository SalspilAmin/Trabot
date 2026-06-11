using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Interactions.Queries.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Helpers.Results;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Features.Interactions.Queries.Handler
{
    public class InteractionQueryHandler :
         
         ResponseHandler,

         IRequestHandler<GetInteractionsByPostIdQuery,Response<List<InteractionWithPostResult>>>,

         IRequestHandler<GetInteractionByIdQuery,Response<InteractionWithPostResult>>,

         IRequestHandler<GetUserInteractionOnPostQuery,Response<InteractionWithPostResult>>
    {
        #region Fields

        private readonly
            IInteractionWithPostService
            interactionService;

        private readonly IMapper mapper;

        private readonly LocalizationService
            localization;

        #endregion

        #region Constructor

        public InteractionQueryHandler(
            IInteractionWithPostService
                interactionService,
            IMapper mapper,
            LocalizationService localization)
            : base(localization)
        {
            this.interactionService =
                interactionService;

            this.mapper = mapper;

            this.localization = localization;
        }

        #endregion

        #region Get By PostId

        public async Task<
            Response<List<InteractionWithPostResult>>>
            Handle(
                GetInteractionsByPostIdQuery request,
                CancellationToken cancellationToken)
        {
            try
            {
                var interactions =
                    await interactionService
                        .GetByPostIdAsync(
                            request.PostId);

                var result =
                    mapper.Map<
                        List<
                            InteractionWithPostResult>>(
                                interactions);

                return Success(result);
            }
            catch (Exception ex)
            {
                return BadRequest<
                    List<
                        InteractionWithPostResult>>(
                            ex.Message);
            }
        }

        #endregion

        #region Get By Id

        public async Task<
            Response<InteractionWithPostResult>>
            Handle(
                GetInteractionByIdQuery request,
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
                    return NotFound<
                        InteractionWithPostResult>(
                            localization.Get(
                                "NotFound"));
                }

                var result =
                    mapper.Map<
                        InteractionWithPostResult>(
                            interaction);

                return Success(result);
            }
            catch (Exception ex)
            {
                return BadRequest<
                    InteractionWithPostResult>(
                        ex.Message);
            }
        }

        #endregion

        #region Get User Interaction

        public async Task<
            Response<InteractionWithPostResult>>
            Handle(
                GetUserInteractionOnPostQuery request,
                CancellationToken cancellationToken)
        {
            try
            {
                var interaction =
                    await interactionService
                        .GetUserInteractionAsync(
                            request.UserId,
                            request.PostId);

                if (interaction == null)
                {
                    return NotFound<
                        InteractionWithPostResult>(
                            localization.Get(
                                "NotFound"));
                }

                var result =
                    mapper.Map<
                        InteractionWithPostResult>(
                            interaction);

                return Success(result);
            }
            catch (Exception ex)
            {
                return BadRequest<
                    InteractionWithPostResult>(
                        ex.Message);
            }
        }

        #endregion
    }
}
