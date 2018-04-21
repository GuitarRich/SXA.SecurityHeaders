using Sitecore.Data;

namespace SXA.Foundation.SecurityHeaders
{
    public struct Templates
    {
        public struct SecurityHeaders
        {
            public static ID Id => new ID("{A6D63CC1-60CC-4E53-8A70-0228A90ED4A6}");
        }

        public struct ContentSecurityPolicy
        {
            public static ID Id => new ID("{14E4990C-0EC4-4F7F-BA83-A3FA8ACDD941}");

            public struct Fields
            {
            }
        }

        public struct Policy
        {
            public static ID Id => new ID("{EC2F720B-E083-49B3-AF4D-65A07B5AF4DA}");

            public struct Fields
            {
                public static ID AllowedHosts => new ID("{7EB4D7AF-0D58-4229-A73A-8F5B2BB91EF2}");
                public static ID AdditionalHosts => new ID("{800AFEF6-E4D9-458D-A25E-A8E309713039}");
            }
        }

        public struct ReferrerPolicy
        {
            public static ID Id => new ID("{125EC14B-94AB-49F5-88CB-F0F9A942ED1D}");

            public struct Fields
            {
                public static ID Policy => new ID("{A3D61AB4-D322-4A94-94D7-CB106AEA45E5}");
            }
        }

        public struct XssProtection
        {
            public static ID Id => new ID("{4CA98C4A-9E1A-40B4-B1BF-475994B184EA}");

            public struct Fields
            {
                public static ID Enabled => new ID("{03888DF2-C647-4339-A822-5B9C85F4C7CE}");
            }
        }

        public struct HttpStrictTransportPolicy
        {
            public static ID Id => new ID("{19FCE6A4-EE24-425B-82A9-C5DEF18C7628}");

            public struct Fields
            {
                public static ID MaxAge => new ID("{F57FD334-1877-4926-8692-4DF1EFC8FB89}");
                public static ID IncludeSubDomains => new ID("{B41B8FC8-8DB8-4254-B3D3-F8766C95BC24}");
                public static ID Preload => new ID("{F386AD34-BC0A-4149-8BAC-26C3F944F357}");
            }
        }

        public struct XContentTypeOptions
        {
            public static ID Id => new ID("{D02155B5-5120-4053-B4DC-00B3D72FDC8C}");

            public struct Fields
            {
                public static ID Enabled => new ID("{E1C72D65-932B-4DB1-A20E-6AB0065CEBE9}");
            }
        }

        public struct XFrameOptions
        {
            public static ID Id => new ID("{EEB45EAF-D9B6-453D-B76A-6AE830DB93D0}");

            public struct Fields
            {
                public static ID Options => new ID("{9AC5A94A-2533-49EB-B4CA-61C434EB9D9D}");
            }
        }
    }
}