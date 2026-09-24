using Microsoft.AspNetCore.Mvc;
using OpticalCenterAPI.Data;
using OpticalCenterAPI.Models;
using Microsoft.EntityFrameworkCore;
using OpticalCenterAPI.DTOs;

namespace OpticalCenterAPI.Controllers

{
    [ApiController] /* This is called an attribute.ASP.NET Core will then enable API-specific features automatically.*/
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatients()
        {
            var patients = await _context.Patients
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    DateOfBirth = p.DateOfBirth
                })
                .ToListAsync();

            return Ok(patients);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetPatient(int id)
        {
            var patient = await _context.Patients
                .Where(p => p.Id == id)
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    DateOfBirth = p.DateOfBirth
                })
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }



        [HttpPost]
        public async Task<ActionResult<PatientDto>> CreatePatient(CreatePatientDto dto)
        {
            var patient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();


            var patientDto = new PatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email,
                DateOfBirth = patient.DateOfBirth
            };


            return CreatedAtAction(
    nameof(GetPatient),
    new { id = patient.Id },
    patientDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDto>> UpdatePatient(
    [FromRoute] int id,
    [FromBody] UpdatePatientDto dto)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            patient.FirstName = dto.FirstName;
            patient.LastName = dto.LastName;
            patient.PhoneNumber = dto.PhoneNumber;
            patient.Email = dto.Email;
            patient.DateOfBirth = dto.DateOfBirth;

            await _context.SaveChangesAsync();

           
        var patientDto = new PatientDto
{
            Id = patient.Id,
             FirstName = patient.FirstName,
            LastName = patient.LastName,
            PhoneNumber = patient.PhoneNumber,
            Email = patient.Email,
            DateOfBirth = patient.DateOfBirth
};

            return Ok(patientDto);


        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}