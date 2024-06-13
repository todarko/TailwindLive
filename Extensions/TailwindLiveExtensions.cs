using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailwindLive.Middleware;

namespace TailwindLive.Extensions
{
    public static class TailwindLiveExtensions
    {
        public static IApplicationBuilder UseGitBashMiddleware(this IApplicationBuilder builder, MiddlewareOptions options)
        {
            return builder.UseMiddleware<TailwindLiveMiddleware>(Options.Create(options));
        }
    }
}
