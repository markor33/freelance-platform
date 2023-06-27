namespace FreelancerProfile.Application.Queries
{
    public record LanguageViewModel
    {
        public int Id { get; private init; }
        public string Name { get; private init; }
        public string ShortName { get; private init; }

        public LanguageViewModel() { }

        public LanguageViewModel(int id, string name, string shortName)
        {
            Id = id;
            Name = name;
            ShortName = shortName;
        }
    }
}
