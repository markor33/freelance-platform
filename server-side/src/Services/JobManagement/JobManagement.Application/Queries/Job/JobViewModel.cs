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
        public Payment Payment { get; set; }
        public int Credits { get; private init; }
        public List<QuestionViewModel> Questions { get; private init; }
        public ProfessionViewModel Profession { get; set; }
        public List<SkillViewModel> Skills { get; private init; }

        public JobViewModel()
        {
            Questions = new List<QuestionViewModel>();
            Skills = new List<SkillViewModel>();
        }

        public JobViewModel(Guid id, string title, string description, ExperienceLevel experienceLevel, 
            Payment payment, int credits, List<QuestionViewModel> questions, ProfessionViewModel profession, List<SkillViewModel> skills)
        {
            Id = id;
            Title = title;
            Description = description;
            ExperienceLevel = experienceLevel;
            Payment = payment;
            Credits = credits;
            Questions = questions;
            Profession = profession;
            Skills = skills;
        }
    }

    public record AnswerViewModel
    {
        public Guid Id { get; private init; }
        public QuestionViewModel Question { get; set; }
        public string Text { get; private init; }

        public AnswerViewModel() { }

        public AnswerViewModel(Guid id, QuestionViewModel question, string text)
        {
            Id = id;
            Question = question;
            Text = text;
        }
    }

    public class QuestionViewModel : IEquatable<QuestionViewModel>
    {
        public Guid Id { get; private init; }
        public string Text { get; private init; }

        public QuestionViewModel() { }

        public QuestionViewModel(Guid id, string text)
        {
            Id = id;
            Text = text;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals((QuestionViewModel)obj);
        }

        public bool Equals(QuestionViewModel? other)
        {
            if (other == null) return false;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }

}
