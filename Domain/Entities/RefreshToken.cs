using Domain.Extensions;

namespace Domain.Entities;

public class RefreshToken : Entity
{
    public bool IsUsed { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresIn { get; set; }
}
