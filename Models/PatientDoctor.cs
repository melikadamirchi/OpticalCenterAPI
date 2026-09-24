namespace OpticalCenterAPI.Models
{
    public class PatientDoctor
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
    }
}
