using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;

namespace JobManagement.Application.Queries
{
    public record JobViewModel
    {
        public Guid Id { get; private init; }
        public string Title { get; private init; }
        public string Description { get; private init; }
        public ExperienceLevel ExperienceLevel { get; private init; }
        public Payment Payment { get; private init; }
        public int Credits { get; private init; }
        public List<QuestionViewModel> Questions { get; private init; }

        public JobViewModel()
        {
            Questions = new List<QuestionViewModel>();
        }

        public JobViewModel(Guid id, string title, string description, ExperienceLevel experienceLevel, 
            Payment payment, int credits, List<QuestionViewModel> questions)
        {
            Id = id;
            Title = title;
            Description = description;
            ExperienceLevel = experienceLevel;
            Payment = payment;
            Credits = credits;
            Questions = questions;
        }
    }

    public record AnswerViewModel
    {
        public Guid Id { get; private init; }
        public Guid QuestionId { get; private init; }
        public string Text { get; private init; }

        public AnswerViewModel() { }

        public AnswerViewModel(Guid id, Guid questionId, string text)
        {
            Id = id;
            QuestionId = questionId;
            Text = text;
        }
    }

    public record QuestionViewModel
    {
        public Guid Id { get; private init; }
        public string Text { get; private init; }

        public QuestionViewModel() { }

        public QuestionViewModel(Guid id, string text)
        {
            Id = id;
            Text = text;
        }
    }

}
