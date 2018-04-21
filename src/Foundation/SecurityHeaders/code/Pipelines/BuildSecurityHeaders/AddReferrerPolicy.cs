using Northwoods.Foundation.SecurityHeaders.Context;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Northwoods.Foundation.SecurityHeaders.Pipelines.BuildSecurityHeaders
{
    public class AddReferrerPolicy : BuildSecurityHeadersProcessor
    {
        private readonly ISecurityHeadersSiteContext _securityHeadersSiteContext;

        public AddReferrerPolicy(ISecurityHeadersSiteContext securityHeadersSiteContext)
        {
            _securityHeadersSiteContext = securityHeadersSiteContext;
        }

        public override void Process(BuildSecurityHeadersArgs args)
        {
            var headers = GenerateHeaders();
            if (!string.IsNullOrWhiteSpace(headers))
            {
                args.Headers.Add("Referrer-Policy", headers);
            }
        }

        private string GenerateHeaders()
        {
            var settings = _securityHeadersSiteContext.GetSettingItem(Templates.ReferrerPolicy.Id);

            return settings == null ? string.Empty : RenderSetting(settings, Templates.ReferrerPolicy.Fields.Policy);
        }

        private static string RenderSetting(Item settings, ID fieldId)
        {
            var targetField = (LookupField)settings.Fields[fieldId];
            return targetField?.TargetItem?["value"] ?? string.Empty;
        }
    }
}