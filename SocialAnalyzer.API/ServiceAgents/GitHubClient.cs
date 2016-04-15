using SocialAnalyzer.API.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SocialAnalyzer.API.ServiceAgents
{
    public class GitHubClient
    {
        public IEnumerable<string> GetRepositories(string userLogin)
        {
            var url = $"users/{userLogin}/repos";
            IEnumerable<string> repositories;
            var request = CreateRequest(url);

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                var content = GetResponseContent(response);
                var json = JArray.Parse(content);
                repositories = json.Select(item => item.Value<string>("full_name")).ToList();
            }
            return repositories;
        }

        public UserViewModel FindUserByEmail(string email)
        {
            var url = $"search/users?q={email}";
            var user = new UserViewModel();
            var request = CreateRequest(url);

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                var content = GetResponseContent(response);
                var result = JsonConvert.DeserializeObject<dynamic>(content);
                user.Email = email;
                user.Login = result.login.ToString();
            }
            return user;
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