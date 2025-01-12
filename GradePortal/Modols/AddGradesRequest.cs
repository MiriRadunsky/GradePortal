namespace GradePortal.Modols
{
    public class AddGradesRequest
    {
        public List<M_Grade> M_Grade { get; set; }
        public List<string> ListId { get; set; }
        public int ExeNumber { get; set; }
    }
}
