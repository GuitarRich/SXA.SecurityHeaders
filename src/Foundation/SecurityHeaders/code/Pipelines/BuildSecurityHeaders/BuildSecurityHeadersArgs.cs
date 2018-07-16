using System.Collections.Generic;
using Sitecore.Pipelines;

// Disabling `Update this implementation of ‘ISerializable’ to conform to the recommended serialization pattern.` 
// as the serialization is inherited from Sitecore code.
#pragma warning disable S3925

namespace SXA.Feature.SecurityHeaders.Pipelines.BuildSecurityHeaders
{
    public class BuildSecurityHeadersArgs : PipelineArgs
    {
        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();
    }
}