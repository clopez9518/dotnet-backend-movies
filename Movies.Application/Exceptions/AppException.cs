
﻿
namespace Movies.Application.Exceptions
{
    public abstract class AppException : Exception
    {
        public int StatusCode { get; set; }
        public string Code { get; set; } = string.Empty;

        protected AppException(string message, int statusCode, string code) : base(message)
        {
            StatusCode = statusCode; 
            Code = code;
        }
    }
}
