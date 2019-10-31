using System;
using System.Net;

namespace {ProjectName}.Exceptions
{
    public class HttpException : Exception
    {
        public int StatusCode { get; }

        public HttpException(HttpStatusCode httpStatusCode)
            : base(httpStatusCode.ToString())
        {
            StatusCode = (int)httpStatusCode;
        }
    }
}
