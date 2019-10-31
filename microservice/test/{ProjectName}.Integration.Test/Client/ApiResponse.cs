using System.Net;

namespace {ProjectName}.Integration.Test.Client
{
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Result { get; set; }
        public string ResultAsString { get; set; }
    }
}