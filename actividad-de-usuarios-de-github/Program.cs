using GithubTypes;
using System.Text.Json;
using System.Globalization;

class Program
{
    static async Task Main()
    {
        await GetUsers("lauta-dev");
    }

    public static async Task GetUsers(string username)
    {
        // Parsear fecha
        var cultureInfo = new CultureInfo("es-AR");
        var format = "dddd 'de' MMMM, HH:mm";

        string url = $"https://api.github.com/users/lauta-dev/events";
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            WriteIndented = true
        };
        try
        {
            /*
              HttpClient client = new HttpClient();

              client.DefaultRequestHeaders.Add("User-Agent", "C# App");
              HttpResponseMessage res = await client.GetAsync(url);

              HttpContent content = res.Content;
              var data = await content.ReadAsStringAsync();
              Github[] gh = JsonSerializer.Deserialize<Github[]>(data, serializeOptions);*/

            StreamReader r = new StreamReader("ghEvent.json");
            string json = r.ReadToEnd();
            Github[] gh = JsonSerializer.Deserialize<Github[]>(json, serializeOptions);
            for (int i = 0; i < gh.Length; i++)
            {
                var ghData = gh[i];
                Console.WriteLine($"Creado el: {Convert.ToDateTime(ghData.CreatedAt, cultureInfo).ToString(format)}");
                Console.WriteLine($"Tipo: {ghData.Type}");
                Console.WriteLine($"Repositorio: {ghData.Repo.Name}");
                Console.WriteLine($"Total de commits: {ghData.Payload.Commits.Length}");

                foreach (var item in ghData.Payload.Commits)
                {
                    Console.WriteLine($"Mensaje: {item.Message} por {item.Author.Name}");
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
