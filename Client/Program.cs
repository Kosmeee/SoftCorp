using System.Net.Http.Headers;
using Client.Actions;
using Client.Tools;


Console.ForegroundColor = ConsoleColor.White;
var url = "https://localhost:7020/api/users/";
var client = new HttpClient();
Uri baseUri = new Uri(url);
client.BaseAddress = baseUri;
var endLine = Environment.NewLine;
while (true)
{
    Console.WriteLine($"Select an option:{endLine}" +
                      $"1 - Authenticate{endLine}" +
                      $"2 - User Info{endLine}" +
                      $"3 - Create User{endLine}" +
                      $"4 - Set Status{endLine}" +
                      $"5 - Remove User{endLine}" +
                      $"6 - Exit{endLine}");
    var key = Console.ReadKey();
    switch (key.Key)
    {
        case ConsoleKey.D1:
            if (Actions.Auth(client, out var userName, out var password))
            {
                var authenticationString = $"{userName}:{password}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                Console.Clear();
                "Authenticated successfully".WriteLineWithColor(ConsoleColor.Green);
                break;
            }
            Console.Clear();
            "Authentication went wrong".WriteLineWithColor(ConsoleColor.DarkRed);
            break;
        case ConsoleKey.D2:
            Actions.UserInfo(client);
            Console.Clear();
            break;
        case ConsoleKey.D3:
            Console.WriteLine(Actions.CreateUser(client));
            break;
        case ConsoleKey.D4:
            Console.WriteLine(Actions.SetStatus(client));
            break;
        case ConsoleKey.D5:
            Console.WriteLine(Actions.RemoveUser(client));
            break;
        case ConsoleKey.D6:
            return;
        default: Console.Clear();
            "Wrong option, try again".WriteLineWithColor(ConsoleColor.DarkRed);
            break;
    }
}