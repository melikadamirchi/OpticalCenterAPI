using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpticalCenterAPI.Data;
using OpticalCenterAPI.DTOs;
using OpticalCenterAPI.Models;
using System.Numerics;

namespace OpticalCenterAPI.Controllers

{
    [ApiController] /* This is called an attribute.ASP.NET Core will then enable API-specific features automatically.*/
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors()
        {
            var doctors = await _context.Doctors
                .Select(d => new DoctorDto
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    PhoneNumber = d.PhoneNumber,
                    Email = d.Email,
                    Specialty = d.Specialty,
                    DateOfBirth = d.DateOfBirth
                })
                .ToListAsync();

            return Ok(doctors);
        }
       
[HttpGet("{id}")]
public async Task<ActionResult<DoctorDto>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors
                .Where(d => d.Id == id)
                .Select(d => new DoctorDto
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    PhoneNumber = d.PhoneNumber,
                    Email = d.Email,
                    Specialty = d.Specialty,
                    DateOfBirth = d.DateOfBirth
                })
                .FirstOrDefaultAsync();

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }


        [HttpPost]
        public async Task<ActionResult<DoctorDto>> CreateDoctor(CreateDoctorDto dto)
        {
            var doctor = new Doctor
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Specialty=dto.Specialty,
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            var doctorDto = new DoctorDto
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                PhoneNumber = doctor.PhoneNumber,
                Email = doctor.Email,
                Specialty = doctor.Specialty,
                DateOfBirth = doctor.DateOfBirth
            };
            return CreatedAtAction(
    nameof(GetDoctor),
    new { id = doctor.Id },
    doctorDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorDto>> UpdateDoctor(
    [FromRoute] int id,
    [FromBody] UpdateDoctorDto dto)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            doctor.FirstName = dto.FirstName;
            doctor.LastName = dto.LastName;
            doctor.PhoneNumber = dto.PhoneNumber;
            doctor.Email = dto.Email;
            doctor.Specialty = dto.Specialty;
            doctor.DateOfBirth = dto.DateOfBirth;

            await _context.SaveChangesAsync();

            var doctorDto = new DoctorDto
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                PhoneNumber = doctor.PhoneNumber,
                Email = doctor.Email,
                Specialty = doctor.Specialty,
                DateOfBirth = doctor.DateOfBirth
            };

            return Ok(doctorDto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}