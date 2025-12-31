using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Bases
{
    public class Response<T>
    {
        public string Message { get; set; }
        public object Meta { get; set; }
        public bool Succeeded { get; set; } 
        public List<string> Errors { get; set; }
        public T Data {  get; set; }

        public Response() { }


        public Response(T data, string message = null) 
        {
            Succeeded = true;
            Message = message;
            Data=data;

        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public Response(string message, bool succeeded)
        {
            Succeeded = succeeded;
            Message = message;
        }
        public Response(List<string> Errors)
        {
            Succeeded=false;
            this.Errors = new List<string>();
            this.Errors.AddRange(Errors);
        }
    }
}
