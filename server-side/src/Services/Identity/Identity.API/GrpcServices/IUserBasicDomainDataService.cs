namespace Identity.API.GrpcServices
{
    public interface IUserBasicDomainDataService
    {
        public Task<UserBasicData> GetBasicDataAsync(Guid userId, string role);
    }
}
