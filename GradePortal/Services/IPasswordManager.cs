namespace GradePortal.Services
{
    public interface IPasswordManager
    {
        bool ValidatePassword(string name, string password);
    }
}