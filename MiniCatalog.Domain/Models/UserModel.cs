using MiniCatalog.Domain.Common;

namespace MiniCatalog.Domain.Models;

public class UserModel : BaseModel
{
    public string IdentityId { get; private set; }
    
    public string Email { get; private set; }
    public string UserName { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    
    public UserModel(string email, string userName, DateOnly dateOfBirth, string identityId)
    {
        Email = email;
        UserName = userName;
        DateOfBirth = dateOfBirth;
        IdentityId = identityId;
    }
}