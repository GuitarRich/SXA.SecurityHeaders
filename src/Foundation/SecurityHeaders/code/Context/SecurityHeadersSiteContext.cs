using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.XA.Foundation.Multisite;

namespace SXA.Foundation.SecurityHeaders.Context
{
    public class SecurityHeadersSiteContext : ISecurityHeadersSiteContext
    {
        private readonly IMultisiteContext _multisiteContext;

        public SecurityHeadersSiteContext(IMultisiteContext multisiteContext)
        {
            _multisiteContext = multisiteContext;
        }

        public Item SecurityHeadersSettingItem =>
            _multisiteContext.SettingsItem?.Children.FirstOrDefault(item =>
                item.TemplateID == Templates.SecurityHeaders.Id);

        public Item GetSettingItem(ID templateId) =>
            SecurityHeadersSettingItem?.Children?.FirstOrDefault(item => item.TemplateID == templateId);
    }
}