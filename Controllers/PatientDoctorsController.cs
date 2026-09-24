using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpticalCenterAPI.Data;
using OpticalCenterAPI.DTOs;
using OpticalCenterAPI.Models;

namespace OpticalCenterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientDoctorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientDoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<PatientDoctor>> CreatePatientDoctor(
     CreatePatientDoctorDto dto)
        {
            var patient = await _context.Patients.FindAsync(dto.PatientId);

            if (patient == null)
            {
                return NotFound($"Patient with ID {dto.PatientId} was not found.");
            }

            var doctor = await _context.Doctors.FindAsync(dto.DoctorId);

            if (doctor == null)
            {
                return NotFound($"Doctor with ID {dto.DoctorId} was not found.");
            }

            var existingConnection = await _context.PatientDoctors
              .FindAsync(dto.PatientId, dto.DoctorId);

            if (existingConnection != null)
            {
                return Conflict("This patient is already connected to this doctor.");
            }
            var connection = new PatientDoctor
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId
            };

            _context.PatientDoctors.Add(connection);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
     nameof(GetPatientDoctors),
     new { },
     connection);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDoctorDto>>> GetPatientDoctors()
        {
            var connections = await _context.PatientDoctors
                .Select(pd => new PatientDoctorDto
                {
                    PatientId = pd.PatientId,
                    PatientName = pd.Patient.FirstName + " " + pd.Patient.LastName,

                    DoctorId = pd.DoctorId,
                    DoctorName = pd.Doctor.FirstName + " " + pd.Doctor.LastName
                })
                .ToListAsync();

            return Ok(connections);
        }

        [HttpGet("{patientId}/{doctorId}")]
        public async Task<ActionResult<PatientDoctorDto>> GetPatientDoctor(
    int patientId, int doctorId)
        {
            var connection = await _context.PatientDoctors
                .Where(pd => pd.PatientId == patientId &&
                             pd.DoctorId == doctorId)
                .Select(pd => new PatientDoctorDto
                {
                    PatientId = pd.PatientId,
                    PatientName = pd.Patient.FirstName + " " + pd.Patient.LastName,
                    DoctorId = pd.DoctorId,
                    DoctorName = pd.Doctor.FirstName + " " + pd.Doctor.LastName
                })
                .FirstOrDefaultAsync();

            if (connection == null)
            {
                return NotFound();
            }

            return Ok(connection);
        }
        [HttpDelete("{patientId}/{doctorId}")]
        public async Task<ActionResult> DeletePatientDoctor(int patientId, int doctorId)
        {
            var connection = await _context.PatientDoctors
                .FindAsync(patientId, doctorId);

            if (connection == null)
            {
                return NotFound();
            }

            _context.PatientDoctors.Remove(connection);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}