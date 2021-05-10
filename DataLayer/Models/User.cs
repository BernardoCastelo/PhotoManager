namespace DataLayer
{
    using Microsoft.AspNetCore.Identity;
    public class User : IdentityUser<int>, IBaseModel
    {
        public string PasswordSalt { get; set; }
    }
}
