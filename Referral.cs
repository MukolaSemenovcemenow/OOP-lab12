namespace MedicalReferral
{
    public class Referral
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string ExaminationType { get; set; }
        public System.DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{PatientName} - {DoctorName} - {ExaminationType} - {Date.ToShortDateString()}";
        }
    }
}
