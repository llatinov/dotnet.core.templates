using System;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace PROJECT_NAME.HealthChecks
{
    public static class HealthCheckExtensions
    {
        public static IEndpointConventionBuilder MapHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    var result = JsonConvert.SerializeObject(
                        new
                        {
                            status = report.Status.ToString(),
                            duration = report.TotalDuration,
                            info = report.Entries.Select(e => new
                            {
                                key = e.Key,
                                description = e.Value.Description,
                                duration = e.Value.Duration,
                                value = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                                error = e.Value.Exception?.Message
                            })
                        }, Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            });
        }
    }
}