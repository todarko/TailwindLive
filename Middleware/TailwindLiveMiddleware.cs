using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Threading.Tasks;
using TailwindLive.Extensions;

namespace TailwindLive.Middleware
{
    public class TailwindLiveMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MiddlewareOptions _options;
        private static bool _isGitBashStarted = false;
        private readonly IWebHostEnvironment _env;

        public TailwindLiveMiddleware(RequestDelegate next, IOptions<MiddlewareOptions> options, IWebHostEnvironment env)
        {
            _next = next;
            _options = options.Value;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_env.IsDevelopment() && !_isGitBashStarted)
            {
                _isGitBashStarted = true;
                StartGitBash();
            }

            await _next(context);
        }

        private void StartGitBash()
        {
            if (!File.Exists(_options.GitBashExe))
            {
                throw new Exception("Can't locate git-bash.exe, please check the GitBashExe string.");
            }

            try
            {
                var buildCss = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = _options.GitBashExe,
                        WorkingDirectory = _options.WorkingDirectory,
                        Arguments = _options.NpmBuildArguments,
                        UseShellExecute = true
                    }
                };

                buildCss.Start();

                buildCss.WaitForExit();

                var watchCss = new ProcessStartInfo
                {
                    FileName = _options.GitBashExe,
                    WorkingDirectory = _options.WorkingDirectory,
                    Arguments = _options.NpmWatchArguments,
                    UseShellExecute = true
                };

                Process.Start(watchCss);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
