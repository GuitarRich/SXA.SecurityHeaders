using System.Linq;
using System.Text;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using SXA.Feature.SecurityHeaders.Context;

namespace SXA.Feature.SecurityHeaders.Pipelines.BuildSecurityHeaders
{
    public class AddContentSecurityPolicy : BuildSecurityHeadersProcessor
    {
        private readonly ISecurityHeadersSiteContext _securityHeadersSiteContext;

        public AddContentSecurityPolicy(ISecurityHeadersSiteContext securityHeadersSiteContext)
        {
            _securityHeadersSiteContext = securityHeadersSiteContext;
        }

        public override void Process(BuildSecurityHeadersArgs args)
        {
            var headers = GenerateHeaders();
            args.Headers.Add("Content-Security-Policy", headers);
        }

        private string GenerateHeaders()
        {
            var cspSettings = _securityHeadersSiteContext.GetSettingItem(Templates.ContentSecurityPolicy.Id);

            if (cspSettings == null)
            {
                return string.Empty;
            }

            var header = new StringBuilder();

            var policies = cspSettings.Children.Where(child => child.TemplateID == Templates.Policy.Id);
            foreach (var policy in policies)
            {
                var source = RenderSetting(policy);
                if (!string.IsNullOrWhiteSpace(source))
                {
                    header.Append(source);
                }
            }

            return header.ToString();
        }

        private static string RenderSetting(Item policy)
        {
            var targetField = (MultilistField)policy.Fields[Templates.Policy.Fields.AllowedHosts];
            if (targetField == null)
            {
                return string.Empty;
            }

            var enums = targetField.GetItems();
            var standardHosts = string.Join(" ", enums.Select(item => item["value"]));
            var additionalHosts = policy[Templates.Policy.Fields.AdditionalHosts];

            if (!string.IsNullOrWhiteSpace(standardHosts) || !string.IsNullOrWhiteSpace(additionalHosts))
            {
                return $"{policy.Name.ToLower()} {standardHosts} {additionalHosts}; ".Trim();
            }

            return string.Empty;
        }
    }
}