
using Newtonsoft.Json;

namespace Microsoft.DotnetOrg.GitHubCaching
{
    public sealed class GitHubCodeOfConduct
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("body")]
        public string Content { get; set; }
    }
}
