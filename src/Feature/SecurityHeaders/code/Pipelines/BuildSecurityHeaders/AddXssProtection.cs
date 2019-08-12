using Sitecore.Data.Fields;
using SXA.Feature.SecurityHeaders.Context;

namespace SXA.Feature.SecurityHeaders.Pipelines.BuildSecurityHeaders
{
    public class AddXssProtection : BuildSecurityHeadersProcessor
    {
        private readonly ISecurityHeadersSiteContext _securityHeadersSiteContext;

        public AddXssProtection(ISecurityHeadersSiteContext securityHeadersSiteContext)
        {
            _securityHeadersSiteContext = securityHeadersSiteContext;
        }

        public override void Process(BuildSecurityHeadersArgs args)
        {
            var headers = GenerateXssHeaders();
            if (!string.IsNullOrEmpty(headers))
            {
                args.Headers.Add("X-XSS-Protection", headers);
            }
        }

        private string GenerateXssHeaders()
        {
            var xssSettings = _securityHeadersSiteContext.GetSettingItem(Templates.XssProtection.Id);
            if (xssSettings == null)
            {
                return string.Empty;
            }

            CheckboxField enabled = xssSettings.Fields[Templates.XssProtection.Fields.Enabled];
            return enabled.Checked ? "1; mode=block" : string.Empty;
        }
    }
}