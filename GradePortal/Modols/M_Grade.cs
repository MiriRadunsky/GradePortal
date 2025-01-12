using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using GradeDO;

namespace GradePortal.Modols
{
    public class M_Grade
    {
        [BindRequired]
        [Range(1, 100)]
        public int ExeNumber { get; set; }//1 ,2 99= for the test
        [Range(1,100)]
        public int GradeNumber { get; set; }
        public Grade Convert()
        {
            Grade g =new Grade()
            {
                ExeNumber = ExeNumber,
                GradeNumber = GradeNumber
            };
            return g;
        }
    }
}
