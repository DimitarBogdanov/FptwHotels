using System.Security.Claims;
using Blazored.SessionStorage;
using FptwHotels.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FptwHotels.Auth;

public sealed class HotelAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly UserManager<UserAccountDto> _userManager;
    private readonly ISessionStorageService _sessionStorageService;

    public HotelAuthenticationStateProvider(UserManager<UserAccountDto> userManager,
        ISessionStorageService sessionStorageService)
    {
        _userManager = userManager;
        _sessionStorageService = sessionStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsIdentity identity = new();
        bool failed = true;

        int userId = await _sessionStorageService.GetItemAsync<int>("userId");

        if (userId > 0)
        {
            UserAccountDto? user = await _userManager.FindByIdAsync(userId.ToString());

            if (user != null)
            {
                failed = false;
                identity = GenerateClaimsIdentity(UserAccount.FromDto(user));
            }
        }

        if (failed)
        {
            await _sessionStorageService.RemoveItemAsync("userId");
        }

        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }

    public async Task StartUserSession(UserAccount user)
    {
        await _sessionStorageService.SetItemAsync("userId", user.UserAccountId);

        ClaimsIdentity identity = GenerateClaimsIdentity(user);
        ClaimsPrincipal userAccount = new(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(userAccount)));
    }

    public async Task EndUserSession()
    {
        await _sessionStorageService.RemoveItemAsync("userId");

        ClaimsIdentity identity = new();

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
    }

    private static ClaimsIdentity GenerateClaimsIdentity(UserAccount user)
    {
        ClaimsIdentity identity = new(new[]
        {
            new Claim("UserAccountId", user.UserAccountId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name)
        }, "HotelManagementAuth");

        return identity;
    }
}