using System.Text.Json;
using System.Globalization;

class Api
{
    private string Path;

    public Api(string PathFile)
    {
        Path = PathFile;
    }

    async Task<JsonScheme[]> GetJson()
    {
        string text = await File.ReadAllTextAsync(Path);
        var json = JsonSerializer.Deserialize<JsonScheme[]>(text);
        return json;
    }

    string ParseDateTime(DateTime date)
    {
        CultureInfo culture = new CultureInfo("es-AR");
        string format = "dd/MM/yyyy, HH:mm";
        return Convert.ToDateTime(date, culture).ToString(format);
    }

    async public Task AddTask(string description)
    {
        var json = await GetJson();
        int getLastId = json.Length < 1
          ? json.Length + 1
          : int.Parse(json[json.Length - 1].Id) + 1;

        string currectId = getLastId.ToString();
        var createdNewObject = new JsonScheme(currectId, description);

        var jsonToList = json.ToList();
        jsonToList.Add(createdNewObject);
        var listToArray = JsonSerializer.Serialize(jsonToList.ToArray());

        await File.WriteAllTextAsync(Path, listToArray);
    }

    public async Task DelTask(string id)
    {
        var json = await GetJson();
        var getObject = json.Where(o => o.Id != id).ToList();
        var listToJson = JsonSerializer.Serialize(getObject);
        await File.WriteAllTextAsync(Path, listToJson);
    }

    public async Task UpdateTask(string id, string newDescripton)
    {
        var json = await GetJson();
        List<JsonScheme> jsonScheme = [];

        foreach (var item in json)
        {
            if (item.Id == id)
            {
                item.Description = newDescripton;
                item.UpdatedAt = DateTime.Now;
            }

            jsonScheme.Add(item);
        }

        var listTojson = JsonSerializer.Serialize(jsonScheme);
        await DelTask(id);
        await File.WriteAllTextAsync(Path, listTojson);
    }

    public async Task SetStatus(string id, string mark)
    {
        var json = await GetJson();
        List<JsonScheme> jsonScheme = [];

        foreach (var item in json)
        {
            if (item.Id == id)
            {
                item.Status = mark;
            }

            jsonScheme.Add(item);
        }

        var listTojson = JsonSerializer.Serialize(jsonScheme);
        await DelTask(id);
        await File.WriteAllTextAsync(Path, listTojson);
    }

    public async Task List(string whyFilter = "all")
    {
        var json = await GetJson();

        foreach (var item in json)
        {
            if (whyFilter == "all")
                System.Console.WriteLine($"""
                    id: {item.Id}
                    Estado: {item.Status}
                    Desc: {item.Description}
                    Creado el: {ParseDateTime(item.CreatedAt)}
                    Actualizado el: {ParseDateTime(item.UpdatedAt)}
                    """);

            else if (whyFilter == "done" && item.Status == "done")
                System.Console.WriteLine($"""
                    id: {item.Id}
                    Estado: {item.Status}
                    Desc: {item.Description}
                    Creado el: {ParseDateTime(item.CreatedAt)}
                    Actualizado el: {ParseDateTime(item.UpdatedAt)}
                    """);

            else if (whyFilter == "todo" && item.Status == "todo")
                System.Console.WriteLine($"""
                    id: {item.Id}
                    Estado: {item.Status}
                    Desc: {item.Description}
                    Creado el: {ParseDateTime(item.CreatedAt)}
                    Actualizado el: {ParseDateTime(item.UpdatedAt)}
                    """);

            else if (whyFilter == "in-progress" && item.Status == "in-progress")
                System.Console.WriteLine($"""
                    id: {item.Id}
                    Estado: {item.Status}
                    Desc: {item.Description}
                    Creado el: {ParseDateTime(item.CreatedAt)}
                    Actualizado el: {ParseDateTime(item.UpdatedAt)}
                    """);
        }
    }
}
