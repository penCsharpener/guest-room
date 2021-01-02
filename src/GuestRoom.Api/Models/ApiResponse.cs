using System.Net;

namespace GuestRoom.Api.Models
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public ApiResponse(HttpStatusCode statusCode, string message = null) : this((int) statusCode, message) { }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad request",
                401 => "You are not authorized",
                404 => "Resource not found",
                500 => "Internal server error",
                _ => null
            };
        }
    }
}