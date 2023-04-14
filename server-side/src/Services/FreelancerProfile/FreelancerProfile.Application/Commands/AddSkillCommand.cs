using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    [DataContract]
    public class AddSkillCommand : IRequest<bool>
    {
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public List<Guid> Skills { get; private set; }

        public AddSkillCommand() { }

        [JsonConstructor]
        public AddSkillCommand(Guid userId, List<Guid> skills)
        {
            UserId = userId;
            Skills = skills;
        }

    }
}
