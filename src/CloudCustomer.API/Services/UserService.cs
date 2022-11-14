using System.Net;
using CloudCustomer.API.Config;
using CloudCustomer.API.Domain;
using Microsoft.Extensions.Options;

namespace CloudCustomer.API.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpclient;
        private readonly UsersApiOptions _apiConfig;

        public UserService(HttpClient httpclient, IOptions<UsersApiOptions> apiConfig)
        {
            _httpclient = httpclient;
            _apiConfig = apiConfig.Value;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userResponse = await _httpclient.GetAsync(_apiConfig.EndPoint);

            if (userResponse.StatusCode == HttpStatusCode.NotFound)
                return new List<User>();

            var responseContent = userResponse.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
            return allUsers.ToList();
        }
    }
}
