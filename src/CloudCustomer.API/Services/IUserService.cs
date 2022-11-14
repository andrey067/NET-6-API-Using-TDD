using CloudCustomer.API.Domain;

namespace CloudCustomer.API.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
    }
}
