namespace OpticalCenterAPI.DTOs
{
    public class PatientDoctorDto
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;

        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
    }
}
