using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailwindLive.Extensions
{
    public class MiddlewareOptions
    {
        public string GitBashExe { get; set; } = "";
        public string? WorkingDirectory { get; set; } = Environment.CurrentDirectory;
        public string? NpmBuildArguments { get; set; } = "-c \"npm run build:css\"";
        public string? NpmWatchArguments { get; set; } = "-c \"npm run watch:css\"";
    }
}
