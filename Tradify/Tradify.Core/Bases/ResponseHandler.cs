using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Resources;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Bases
{
    public class ResponseHandler
    {
        private readonly LocalizationService _localize;

        public ResponseHandler(LocalizationService localization) { _localize = localization; }

        public Response<T>  Success<T>(T entity, object meta=null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Meta = meta,
                Message = _localize.Get("Success")

            };

        }
        public Response<T> Unauthorized<T>(string message = null) 
        {
            return new Response<T>()
            {
                Message = message==null? _localize.Get("UnAuthorized"):message,
                StatusCode= System.Net.HttpStatusCode.Unauthorized,
                Succeeded = false,


            };
        
        }
 
        public Response<T> Deleted<T>(string message=null)
        {
            return new Response<T>()
            {

                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = message == null ? _localize.Get("Deleted"):message,


            };

        }

        public Response<T> BadRequest<T>(string messag=null)
        {
            return new Response<T>() 
            
            {
                Message= messag==null ? _localize.Get("BadRequest") : messag,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded=false
            
            };

        }

        public Response<T> UnprocessableEntity<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = message == null ? _localize.Get("UnprocessableEntity"): message
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? _localize.Get("NotFound") : message
            };
        }

        public Response<T> Created<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = _localize.Get("Created"),
                Meta = Meta
            };

        }
}
