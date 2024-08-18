namespace FptwHotels.Data;

public sealed class UserAccount
{
    public int UserAccountId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public static UserAccount FromDto(UserAccountDto accountDto) => new()
    {
        UserAccountId = accountDto.Id,
        Name = accountDto.Name,
        Email = accountDto.Email ?? "",
    }; 
}