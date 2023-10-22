using Elastic.Clients.Elasticsearch;
using JobSearch.Abstractions.Model;

namespace JobSearch.Elastic.Mappings
{
    public static class JobMapping
    {
        public static void Create(ElasticsearchClient client)
        {
            client.Indices.Delete("job");
            var res = client.Indices.Create<Job>("job", c => 
                c.Mappings(
                    m => m.Properties(ps => ps
                        .Text(s => s.Title)
                        .Text(s => s.Description)
                        .IntegerNumber(s => s.Credits)
                        .IntegerNumber(s => s.NumOfProposals)
                        .Date(s => s.Created)
                        .Keyword(s => s.ExperienceLevel)
                        .Keyword(s => s.Status)
                        .Keyword(s => s.ProfessionId)
                        .Keyword(s => s.Skills))));
        }
    }
}
