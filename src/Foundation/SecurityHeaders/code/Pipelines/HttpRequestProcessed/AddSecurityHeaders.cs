using Sitecore.Pipelines;
using Sitecore.Pipelines.HttpRequest;
using SXA.Foundation.SecurityHeaders.Pipelines.BuildSecurityHeaders;

namespace SXA.Foundation.SecurityHeaders.Pipelines.HttpRequestProcessed
{
    public class AddSecurityHeaders : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (args.HttpContext.Response.HeadersWritten ||
                Sitecore.Context.Site == null ||
                Sitecore.Context.Site.Name == "shell" ||
                Sitecore.Context.Item == null ||
                !Sitecore.Context.PageMode.IsNormal)
            {
                return;
            }

            // Execute the pipeline
            var pipelineArgs = new BuildSecurityHeadersArgs();
            CorePipeline.Run("buildSecurityHeaders", pipelineArgs);

            foreach (var header in pipelineArgs.Headers)
            {
                args.HttpContext.Response.AddHeader(header.Key, header.Value);
            }
        }
    }
}
