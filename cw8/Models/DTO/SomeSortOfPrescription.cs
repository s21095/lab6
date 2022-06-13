namespace cw8.Models.DTO
{
    public class SomeSortOfPrescription
    {
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public SomeSortOfPatient Patient { get; set; }
        public SomeSortOfDoctor Doctor { get; set; }
        public virtual IEnumerable<SomeSortOfMedicament> Medicaments { get; set; }
    }
}
