using System.Text;
using FptwHotels.Data;
using Microsoft.AspNetCore.Identity;

namespace FptwHotels.Auth;

public sealed class AccountService : IAccountService
{
    private readonly UserManager<UserAccountDto> _userManager;
    private readonly SignInManager<UserAccountDto> _signInManager;
    
    public AccountService(
        UserManager<UserAccountDto> userManager,
        SignInManager<UserAccountDto> signInManager
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<UserAccount> RegisterUser(UserAccount userAccount, string password)
    {
        UserAccountDto? user = await _userManager.FindByEmailAsync(userAccount.Email);

        if (user != null)
        {
            throw new Exception("User already exists");
        }

        UserAccountDto newUser = new()
        {
            Name = userAccount.Name,
            Email = userAccount.Email,
            UserName = userAccount.Email,
        };

        IdentityResult result = await _userManager.CreateAsync(newUser, password);

        user = await _userManager.FindByEmailAsync(userAccount.Email);
        
        if (!result.Succeeded || user == null)
        {
            if (result.Errors.Any())
            {
                throw new Exception(result.Errors.First().Description);
            }
            throw new Exception("Unable to create user account");
        }

        return UserAccount.FromDto(user);
    }

    public async Task<UserAccount> LoginUser(string email, string password)
    {
        UserAccountDto? user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            throw new Exception("Invalid username or password.");
        }

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (result.Succeeded)
        {
            return UserAccount.FromDto(user);
        }
        
        throw new Exception("Invalid username or password.");
    }
}