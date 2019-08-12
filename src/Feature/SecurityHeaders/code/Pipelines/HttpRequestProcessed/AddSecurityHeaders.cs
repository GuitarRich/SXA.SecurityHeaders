using System.Collections.Generic;
using Newtonsoft.Json;
using SXA.Feature.SecurityHeaders.Context;
using Sitecore.Abstractions;
using Sitecore.Pipelines;
using Sitecore.Pipelines.HttpRequest;
using SXA.Feature.SecurityHeaders.Pipelines.BuildSecurityHeaders;
using Sitecore.Sites;

namespace SXA.Feature.SecurityHeaders.Pipelines.HttpRequestProcessed
{
    public class AddSecurityHeaders : HttpRequestProcessor
    {
        private readonly BaseCacheManager _cacheManager;

        public AddSecurityHeaders(BaseCacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

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

            var httpCache = _cacheManager.GetHtmlCache(SiteContext.Current);
            var cacheKey = $"{SiteContext.Current.Name}_#SecurityHeaders#";
            var headerJson = httpCache.GetHtml(cacheKey);
            Dictionary<string, string> headers;

            if (string.IsNullOrWhiteSpace(headerJson))
            {
                // Execute the pipeline
                var pipelineArgs = new BuildSecurityHeadersArgs();
                CorePipeline.Run("buildSecurityHeaders", pipelineArgs);
                headers = pipelineArgs.Headers;
                headerJson = JsonConvert.SerializeObject(headers);
                httpCache.SetHtml(cacheKey, headerJson);
            }
            else
            {
                headers = JsonConvert.DeserializeObject<Dictionary<string, string>>(headerJson);
            }

            foreach (var header in headers)
            {
                if (!string.IsNullOrWhiteSpace(header.Value))
                {
                    args.HttpContext.Response.AddHeader(header.Key, header.Value);
                }
            }
        }
    }
}
