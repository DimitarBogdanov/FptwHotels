using FptwHotels.Data;

namespace FptwHotels.Auth;

public interface IAccountService
{
    Task<UserAccount> RegisterUser(UserAccount user, string password);

    Task<UserAccount> LoginUser(string email, string password);
}