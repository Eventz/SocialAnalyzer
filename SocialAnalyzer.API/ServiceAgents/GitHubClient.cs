using System.Net;

namespace SocialAnalyzer.API.ServiceAgents
{
    public class GitHubClient
    {


        public IEnumerable<string> GetRepositories(string userLogin)
        {
            var url = $"users/{userLogin}/repos";
            IEnumerable<string> repositories;

            using (var response = CreateRequest(url))
            {
                var content = GetResponseContent(response);
                var json = JArray.Parse(content);
                repositories = json.Select(item => item.Value<string>("full_name")).ToList();
            }

            return repositories;
        }
                
        private string GetResponseContent(HttpWebResponse response)
        {
            var reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd();
        }

        private HttpWebRequest CreateRequest(string fill)
        {
            const string gitHubToken = "";
            const string urlApiGitHub = "https://api.github.com";
            var url = $"{urlApiGitHub}/{fill}?access_token={gitHubToken}";
            var request = WebRequest.Create(url) as HttpWebRequest;

            if (request != null) request.UserAgent = "SocialAnalyzer";

            return request;
        }
    }
}