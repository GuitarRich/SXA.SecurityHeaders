using System.Text;
using Sitecore.Data.Fields;
using SXA.Foundation.SecurityHeaders.Context;

namespace SXA.Foundation.SecurityHeaders.Pipelines.BuildSecurityHeaders
{
    public class AddStrictTransportSecurity : BuildSecurityHeadersProcessor
    {
        private readonly ISecurityHeadersSiteContext _securityHeadersSiteContext;

        public AddStrictTransportSecurity(ISecurityHeadersSiteContext securityHeadersSiteContext)
        {
            _securityHeadersSiteContext = securityHeadersSiteContext;
        }

        public override void Process(BuildSecurityHeadersArgs args)
        {
            var headers = GenerateHeaders();
            if (!string.IsNullOrEmpty(headers))
            {
                args.Headers.Add("Strict-Transport-Security", headers);
            }
        }

        protected virtual string GenerateHeaders()
        {
            const string preloadKeyword = "preload";
            const string includeSubDomainsKeyWord = "includeSubDomains";

            var settings = _securityHeadersSiteContext.GetSettingItem(Templates.HttpStrictTransportPolicy.Id);
            if (settings == null)
            {
                return string.Empty;
            }

            var maxAge = settings[Templates.HttpStrictTransportPolicy.Fields.MaxAge];
            CheckboxField includeSubDomains = settings.Fields[Templates.HttpStrictTransportPolicy.Fields.IncludeSubDomains];
            CheckboxField preload = settings.Fields[Templates.HttpStrictTransportPolicy.Fields.IncludeSubDomains];

            var header = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(maxAge))
            {
                header.Append($"max-age={maxAge}; ");
            }
            if (includeSubDomains.Checked)
            {
                header.Append($"{includeSubDomainsKeyWord}; ");
            }
            if (preload.Checked)
            {
                header.Append(preloadKeyword);
            }

            return header.ToString().Trim();
        }
    }
}