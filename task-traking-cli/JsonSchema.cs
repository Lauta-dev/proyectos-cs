class JsonScheme
{
    public string Id { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }

    public JsonScheme(string id, string description, string status = "todo")
    {
        this.Id = id;
        this.Description = description;
        this.Status = status;
    }
}
