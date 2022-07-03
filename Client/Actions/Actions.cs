using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Client.Tools;

namespace Client.Actions
{
    public static class Actions
    {
         static readonly string EndLine = Environment.NewLine;
        public static bool Auth(HttpClient client, out string userName, out string password)
        {
            Console.Clear();
            Console.Write($"Authentication:{EndLine}{EndLine}Username:");
            userName = Console.ReadLine();
            Console.Write($"{EndLine}Password:");
            password = Console.ReadLine();
            if(userName == null || password == null)
                return false;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.Json));
            var credentials = new { userName, password };
            var response = client.PostAsJsonAsync("authenticate", credentials).Result;
            return response.IsSuccessStatusCode;

        }

        public static string CreateUser(HttpClient client)
        {
            var id = Reader.ReadInt("Create User");

            var name = Reader.ReadName();
            var status = Reader.ReadStatus();
            Console.Clear();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.Xml));
            var user = new { id, name, status };
            var userXml = user.ToXml();
            Console.WriteLine($"XML code:{EndLine}{userXml}");
            var httpContent = new StringContent(userXml.ToString(), Encoding.UTF8, Constants.Xml);
            var response = client.PostAsync("createuser", httpContent).Result;
            return response.Content.ReadAsStringAsync().Result;

        }

        public static string RemoveUser(HttpClient client)
        {
            var Id = Reader.ReadInt("Remove User");
            Console.Clear();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.Json));
            var removeUser = new { RemoveUser = new {Id}};
            var response = client.PostAsJsonAsync("removeuser", removeUser).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public static void UserInfo(HttpClient client)
        {
            var id = Reader.ReadInt("User Info");
            Process.Start(new ProcessStartInfo{FileName = client.BaseAddress + $"userinfo?id={id}", UseShellExecute = true});
        }

        public static string SetStatus(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(Constants.Json));
            var Id = Reader.ReadInt("Set Status").ToString();
            var status = Reader.ReadStatus();
            Console.Clear();
            var values = new List<KeyValuePair<string, string>>
            {
                new("Id", Id),
                new("Status", status)
            };
            var content = new FormUrlEncodedContent(values);
            content.Headers.ContentType = new MediaTypeHeaderValue(Constants.Form);
            var response = client.PostAsync("setstatus", content);
            return response.Result.Content.ReadAsStringAsync().Result;

            
        }
        
    }
}
