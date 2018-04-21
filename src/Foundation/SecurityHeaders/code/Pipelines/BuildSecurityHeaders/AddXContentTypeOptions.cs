using Sitecore.Data.Fields;
using SXA.Foundation.SecurityHeaders.Context;

namespace SXA.Foundation.SecurityHeaders.Pipelines.BuildSecurityHeaders
{
    public class AddXContentTypeOptions : BuildSecurityHeadersProcessor
    {
        private readonly ISecurityHeadersSiteContext _securityHeadersSiteContext;

        public AddXContentTypeOptions(ISecurityHeadersSiteContext securityHeadersSiteContext)
        {
            _securityHeadersSiteContext = securityHeadersSiteContext;
        }

        public override void Process(BuildSecurityHeadersArgs args)
        {
            var headers = GenerateHeaders();
            if (!string.IsNullOrEmpty(headers))
            {
                args.Headers.Add("X-Content-Type-Options", headers);
            }
        }

        private string GenerateHeaders()
        {
            var settings = _securityHeadersSiteContext.GetSettingItem(Templates.XContentTypeOptions.Id);
            if (settings == null)
            {
                return string.Empty;
            }

            CheckboxField enabled = settings.Fields[Templates.XContentTypeOptions.Fields.Enabled];
            return enabled.Checked ? "nosniff" : string.Empty;
        }
    }
}