using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tradify.Core.Bases;

namespace Tradify.Bases
{
    [ApiController]
    public abstract class AppControllerBase : ControllerBase
    {
        //private  IMediator _meduator;
        //protected IMediator _Mediator => _meduator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected readonly IMediator Mediator;

        protected AppControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }
        public ObjectResult NewResult<T>(Response<T> response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(response),
                HttpStatusCode.Created => Created(string.Empty, response),
                HttpStatusCode.Unauthorized => Unauthorized(response),
                HttpStatusCode.BadRequest => BadRequest(response),
                HttpStatusCode.NotFound => NotFound(response),
                HttpStatusCode.Accepted => Accepted(response),
                HttpStatusCode.UnprocessableEntity => UnprocessableEntity(response),
                _ => BadRequest(response)// default case
            };
        }

    }
}
