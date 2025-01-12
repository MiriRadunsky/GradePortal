using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;
using GradeDO;
using System.ComponentModel.DataAnnotations;

namespace GradePortal.Modols
{
    public class M_Student
    {
        [BindRequired]
        [StringLength(9, MinimumLength = 9)]
        [RegularExpression(@"^\d{9}$")]
        public string ID { get; set; }
        [StringLength(20,MinimumLength = 2)]
        public string Name { get; set; }
        [BindRequired]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        [StringLength(10, MinimumLength = 5)]
        public string Password { get; set; }
        public Student Convert()
        {
            Student S = new Student()
            {
                ID = ID,
                Name = Name
            };
            Password=ID;
            return S;
        }
    }
}
