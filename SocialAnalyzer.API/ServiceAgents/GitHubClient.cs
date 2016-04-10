using System.Net;

namespace SocialAnalyzer.API.ServiceAgents
{
    public class GitHubClient
    {
        public HttpWebRequest CreateRequest(string fill)
        {
            const string gitHubToken = "";
            const string urlApiGitHub = "https://api.github.com";
            var url = $"{urlApiGitHub}/users/{fill}?access_token={gitHubToken}";
            var request = WebRequest.Create(url) as HttpWebRequest;

            if (request != null) request.UserAgent = "SocialAnalyzer";

            return request;
        }
    }
}