using Microsoft.AspNetCore.Identity;

namespace FptwHotels.Data;

public sealed class UserAccountDto : IdentityUser<int>
{
    [PersonalData]
    public string Name { get; set; } = "";
}