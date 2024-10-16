using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        string jsonPath = CreateJsonFile();
        Api api = new Api(jsonPath);

        try
        {
            switch (args[1])
            {
                case "add":
                    await api.AddTask(args[2]);
                    break;

                case "del" or "delete":
                    await api.DelTask(args[2]);
                    break;

                case "upd" or "update":
                    await api.UpdateTask(args[2], args[3]);
                    break;

                case "mark":
                    await api.SetStatus(args[2], args[3]);
                    break;

                case "list":
                    try
                    {
                        await api.List(args[2]);
                    }
                    catch (System.Exception)
                    {
                        await api.List();
                    }
                    break;

                default:
                    Console.WriteLine($"case {args[1]} no existe");
                    break;

            }
        }
        catch (System.IndexOutOfRangeException ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }

    }

    static public string CreateJsonFile(string customPath = "")
    {
        string homeEnv = Environment.GetEnvironmentVariable("HOME");

        if (homeEnv == null)
        {
            return null;
        }

        string jsonDir = $"{homeEnv}/.local/share/todo.json";

        if (!string.IsNullOrEmpty(customPath))
        {
            jsonDir = customPath;
        }

        if (!File.Exists(jsonDir))
        {
            File.Create(jsonDir).Dispose();
        }

        InicialValue(jsonDir);

        return jsonDir;
    }

    static public void InicialValue(string path)
    {
        string initial = "[]";
        string jsonCurrentLines = File.ReadAllText(path);

        if (string.IsNullOrEmpty(jsonCurrentLines))
        {
            var file = File.Open(path, FileMode.Open);
            byte[] bytes = Encoding.ASCII.GetBytes(initial);
            file.Write(bytes);
            file.Close();
        }

    }

}
