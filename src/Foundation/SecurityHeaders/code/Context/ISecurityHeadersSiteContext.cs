using Sitecore.Data;
using Sitecore.Data.Items;

namespace SXA.Foundation.SecurityHeaders.Context
{
    public interface ISecurityHeadersSiteContext
    {
        Item SecurityHeadersSettingItem { get; }

        Item GetSettingItem(ID templateId);
    }
}
