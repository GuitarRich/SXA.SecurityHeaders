using System.Linq;
using System.Text;
using SXA.Feature.SecurityHeaders.Context;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace SXA.Feature.SecurityHeaders.Pipelines.BuildSecurityHeaders
{
    public class AddContentSecurityPolicy : BuildSecurityHeadersProcessor
    {
        private const string ContentSecurityPolicyHeader = "Content-Security-Policy";
        private const string ContentSecurityPolicyReportOnlyHeader = "Content-Security-Policy-Report-Only";

        private readonly ISecurityHeadersSiteContext _securityHeadersSiteContext;

        public AddContentSecurityPolicy(ISecurityHeadersSiteContext securityHeadersSiteContext)
        {
            _securityHeadersSiteContext = securityHeadersSiteContext;
        }

        public override void Process(BuildSecurityHeadersArgs args)
        {
            var cspSettings = _securityHeadersSiteContext.GetSettingItem(Templates.ContentSecurityPolicy.Id);
            if (cspSettings == null)
            {
                return;
            }

            var headers = GenerateHeaders(cspSettings);
            if (!string.IsNullOrWhiteSpace(headers))
            {
                CheckboxField reportOnlyMode = cspSettings.Fields[Templates.ContentSecurityPolicy.Fields.ReportOnly];
                var header = reportOnlyMode.Checked
                    ? ContentSecurityPolicyReportOnlyHeader
                    : ContentSecurityPolicyHeader;
                args.Headers.Add(header, headers);
            }
        }

        private string GenerateHeaders(Item cspSettings)
        {

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

            return header.ToString().Replace("  ", " ");
        }

        private static string RenderSetting(Item policy)
        {
            var targetField = (MultilistField)policy.Fields[Templates.Policy.Fields.AllowedHosts];
            if (targetField == null)
            {
                return string.Empty;
            }

            var enums = targetField.GetItems();
            var standardHosts = string.Join(" ", enums.Select(item => item["value"]))?.Trim();
            var additionalHosts = policy[Templates.Policy.Fields.AdditionalHosts]?.Trim();

            if (!string.IsNullOrWhiteSpace(standardHosts) || !string.IsNullOrWhiteSpace(additionalHosts))
            {
                return $"{policy.Name.ToLower()} {standardHosts} {additionalHosts}; ".Replace(" ;", "; ");
            }

            return string.Empty;
        }
    }
}