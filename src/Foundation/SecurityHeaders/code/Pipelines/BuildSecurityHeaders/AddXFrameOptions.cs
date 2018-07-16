using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using SXA.Feature.SecurityHeaders.Context;

namespace SXA.Feature.SecurityHeaders.Pipelines.BuildSecurityHeaders
{
    public class AddXFrameOptions : BuildSecurityHeadersProcessor
    {
        private readonly ISecurityHeadersSiteContext _securityHeadersSiteContext;

        public AddXFrameOptions(ISecurityHeadersSiteContext securityHeadersSiteContext)
        {
            _securityHeadersSiteContext = securityHeadersSiteContext;
        }

        public override void Process(BuildSecurityHeadersArgs args)
        {
            var headers = GenerateHeaders();
            if (!string.IsNullOrEmpty(headers))
            {
                args.Headers.Add("X-Frame-Options", headers);
            }
        }

        private string GenerateHeaders()
        {
            var settings = _securityHeadersSiteContext.GetSettingItem(Templates.XFrameOptions.Id);
            return settings == null ? 
                string.Empty : 
                RenderEnumSetting(settings, Templates.XFrameOptions.Fields.Options);
        }

        private static string RenderEnumSetting(Item cspSettings, ID fieldId)
        {
            var targetField = (MultilistField)cspSettings.Fields[fieldId];
            if (targetField == null)
            {
                return string.Empty;
            }

            var enums = targetField.GetItems();
            return string.Join(" ", enums.Select(item => item["value"]));
        }
    }
}