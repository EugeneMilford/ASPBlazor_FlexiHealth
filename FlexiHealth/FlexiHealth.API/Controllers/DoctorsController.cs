using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlexiHealth.API.Data;
using FlexiHealth.API.Models;
using AutoMapper;
using FlexiHealth.API.Models.Doctor;
using FlexiHealth.API.Static;

namespace FlexiHealth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly FlexiHealthDbContext _context;
        private readonly IMapper mapper;
        private readonly ILogger<DoctorsController> logger;

        public DoctorsController(FlexiHealthDbContext context, IMapper mapper, ILogger<DoctorsController> logger)
        {
            _context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors()
        {
            try
            {
                var doctors = await _context.Doctors.ToListAsync();
                var doctorDtos = mapper.Map<IEnumerable<DoctorDto>>(doctors);
                return Ok(doctorDtos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing GET in {nameof(GetDoctors)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetDoctor(int id)
        {
            try
            {
                var doctor = await _context.Doctors.FindAsync(id);

                if (doctor == null)
                {
                    return NotFound();
                }

                var doctorDto = mapper.Map<DoctorDto>(doctor);
                return Ok(doctorDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing GET in {nameof(GetDoctor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, UpdateDoctorDto doctorDto)
        {
            try
            {
                if (id != doctorDto.DoctorId)
                {
                    return BadRequest("Doctor ID mismatch");
                }

                var doctor = await _context.Doctors.FindAsync(id);

                if (doctor == null)
                {
                    return NotFound();
                }

                mapper.Map(doctorDto, doctor);
                _context.Entry(doctor).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing PUT in {nameof(PutDoctor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // POST: api/Doctors
        [HttpPost]
        public async Task<ActionResult<DoctorDto>> PostDoctor(AddDoctorDto doctorDto)
        {
            try
            {
                var doctor = mapper.Map<Doctor>(doctorDto);
                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();

                var createdDoctorDto = mapper.Map<DoctorDto>(doctor);
                return CreatedAtAction(nameof(GetDoctor), new { id = doctor.DoctorId }, createdDoctorDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing POST in {nameof(PostDoctor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
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
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing DELETE in {nameof(DeleteDoctor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.DoctorId == id);
        }
    }
}
