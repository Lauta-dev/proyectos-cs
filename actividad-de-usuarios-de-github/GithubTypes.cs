namespace GithubTypes
{

    public partial class Github
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public Actor Actor { get; set; }
        public Repo Repo { get; set; }
        public Payload Payload { get; set; }
        public bool Public { get; set; }
        public string CreatedAt { get; set; }
    }

    public partial class Actor
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string DisplayLogin { get; set; }
        public string GravatarId { get; set; }
        public Uri Url { get; set; }
        public Uri AvatarUrl { get; set; }
    }

    public partial class Payload
    {
        public long RepositoryId { get; set; }
        public long PushId { get; set; }
        public long Size { get; set; }
        public long DistinctSize { get; set; }
        public string Ref { get; set; }
        public string Head { get; set; }
        public string Before { get; set; }
        public Commit[] Commits { get; set; }
    }

    public partial class Commit
    {
        public string Sha { get; set; }
        public Author Author { get; set; }
        public string Message { get; set; }
        public bool Distinct { get; set; }
        public Uri Url { get; set; }
    }

    public partial class Author
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public partial class Repo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Uri Url { get; set; }
    }
}

