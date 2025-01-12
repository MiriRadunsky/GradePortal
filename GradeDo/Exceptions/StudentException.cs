using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeDO.Exceptions
{
    public class StudentNotExsistException : Exception
    {
        public int StatusCode { get; }
        public StudentNotExsistException(string StudentId):base($"The student with Id {StudentId} not exsist.")
        {
            StatusCode = 770;
        }
        
    }
    public class StudentAlradyExsistException  : Exception
    {
        public int StatusCode { get; }

        public StudentAlradyExsistException(string StudentId) : base($"The student with Id {StudentId} aleady exsist.")
        {
            StatusCode = 213;
        }

    }

    public class GradeNotExsistException : Exception
    {
        public int StatusCode { get; }

      public GradeNotExsistException(int codeExe) : base($"The grade with the code {codeExe} not exist.") 
        {
            StatusCode = 456;
        }

    }

    public class ListsAreNotEqualInLenghException:Exception
    {
        public int StatusCode { get; }
        public ListsAreNotEqualInLenghException() : base($"The number of grades must match the number of students.")
        {
            StatusCode = 102;
        }
    }
}
