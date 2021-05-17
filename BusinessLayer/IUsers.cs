using DataLayer;

namespace BusinessLayer
{
    public interface IUsers
    {
        User Insert(string userName, string password);
        User Validate(string userName, string password);
    }
}