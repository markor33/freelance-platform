namespace JobManagement.Application.Queries
{
    public class JobViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public JobViewModel() { }

        public JobViewModel(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

    }
}
