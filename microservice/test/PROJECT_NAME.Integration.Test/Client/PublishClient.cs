using System.Net.Http;
using System.Threading.Tasks;
using PROJECT_NAME.Sqs.Models;

namespace PROJECT_NAME.Integration.Test.Client
{
    public class PublishClient : BaseClient
    {
        public PublishClient(HttpClient httpClient)
            : base(httpClient) { }

        public async Task<ApiResponse<string>> PublishMovie(Movie movie)
        {
            return await SendAsync<string>(HttpMethod.Post, "api/publish/movie", movie);
        }

        public async Task<ApiResponse<string>> PublishActor(Actor actor)
        {
            return await SendAsync<string>(HttpMethod.Post, "api/publish/actor", actor);
        }
    }
}