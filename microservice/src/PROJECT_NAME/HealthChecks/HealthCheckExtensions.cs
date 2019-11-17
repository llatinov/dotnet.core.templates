﻿using System;
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
                        new HealthResult
                        {
                            Status = report.Status.ToString(),
                            Duration = report.TotalDuration,
                            Info = report.Entries.Select(e => new HealthInfo
                            {
                                Key = e.Key,
                                Description = e.Value.Description,
                                Duration = e.Value.Duration,
                                Status = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                                Error = e.Value.Exception?.Message
                            }).ToList()
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