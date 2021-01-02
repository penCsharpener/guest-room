﻿using System.Collections.Generic;

namespace GuestRoom.Api.Models
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(400) { }

        public IEnumerable<string> Errors { get; set; }
    }
}