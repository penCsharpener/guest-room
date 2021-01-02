using System.Net;

namespace GuestRoom.Api.Models
{
    public class ApiExceptionResponse : ApiResponse
    {
        public ApiExceptionResponse(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public ApiExceptionResponse(HttpStatusCode statusCode, string message = null, string details = null) :
            this((int) statusCode, message, details) { }

        public string Details { get; }
    }
}