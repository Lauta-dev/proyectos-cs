using GithubTypes;
using System.Text.Json;
using System.Globalization;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Writte a user from GitHub");
            return;
        }

        string user = args[0];

        await GetUsers(user);
    }

    //TODO: Mejorar el código

    public static async Task GetUsers(string username, int perPage = 5)
    {
        // Parsear fecha
        var cultureInfo = new CultureInfo("es-AR");
        var format = "dddd dd 'de' MMMM, HH:mm";

        string url = $"https://api.github.com/users/{username}/events?per_page={perPage}";
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            WriteIndented = true
        };
        try
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "C# App");
            HttpResponseMessage res = await client.GetAsync(url);

            if (!res.IsSuccessStatusCode)
            {
                Console.WriteLine($"User {username} no exists");
                return;
            }

            HttpContent content = res.Content;
            var data = await content.ReadAsStringAsync();
            Github[] gh = JsonSerializer.Deserialize<Github[]>(data, serializeOptions);

            for (int i = 0; i < gh.Length; i++)
            {
                var ghData = gh[i];
                Console.WriteLine($"- Creado el: {Convert.ToDateTime(ghData.CreatedAt, cultureInfo).ToString(format)}");
                Console.WriteLine($"- Tipo: {ghData.Type}");
                Console.WriteLine($"- Repositorio: {ghData.Repo.Name}");

                if (ghData.Payload.Commits != null)
                {
                    Console.WriteLine($"- Total de commits: {ghData.Payload.Commits.Length}");

                    foreach (var item in ghData.Payload.Commits)
                    {
                        Console.WriteLine($"- Mensaje: {item.Message} por {item.Author.Name}");
                    }
                }


                Console.WriteLine("---------------------------------");

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception line-----");
            Console.WriteLine(ex);
        }
    }
}
