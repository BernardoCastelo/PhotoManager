using DataLayer;

namespace BusinessLayer
{
    public interface IUsers
    {
        User Validate(string userName, string password);
    }
}