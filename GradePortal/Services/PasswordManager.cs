using GradeDO;
using GradeDO.Exceptions;
using GradePortal.Configurations;
using Microsoft.Extensions.Options;

namespace GradePortal.Services
{
    public class PasswordManager : IPasswordManager
    {
        private readonly Techer _teacher;
        private readonly IStudents _students;

        public PasswordManager(IOptions<Techer> techer, IStudents students)
        {
            _teacher = techer.Value;
            _students = students;
        }

        public bool ValidatePassword(string id, string password)
        {
            Student s = _students.GetAllStudents().Find(s => s.ID == id);
            if (s == null)
            {
                if (id == _teacher.TecherID)
                {
                    return _teacher.TecherPassword.Equals(password);
                }
                throw new StudentNotExsistException(id);
            }
            return s.Password.Equals(password);
        }
    }
}

