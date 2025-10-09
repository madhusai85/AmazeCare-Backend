using AmazeCare.Exceptions;
using AmazeCare.Interfaces;
using AmazeCare.Models;
using AmazeCare.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AmazeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentAdminService _appointmentAdminService;
        private readonly IAppointmentUserService _appointmentUserService;

        public AppointmentController(IAppointmentAdminService appointmentAdminService,
                                     IAppointmentUserService appointmentUserService)
        {
            _appointmentAdminService = appointmentAdminService;
            _appointmentUserService = appointmentUserService;
        }

        [Authorize(Roles = "Admin, Doctor")]
        [HttpGet("ViewAllTheAppointments")]
        public async Task<IActionResult> GetAppointment()
        {
            try
            {
                var appointments = await _appointmentAdminService.GetAppointmentList();
                return Ok(appointments);
            }
            catch (Exception)
            {
                return StatusCode(500, "Unable to fetch appointments. Please try again later.");
            }
        }

        [Authorize]
        [HttpGet("ViewAppointmentByAppointmentId")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            try
            {
                var appointment = await _appointmentUserService.GetAppointment(id);
                if (appointment == null)
                    return NotFound("Appointment not found.");
                return Ok(appointment);
            }
            catch (Exception)
            {
                return BadRequest("Invalid request. Please check your input.");
            }
        }

        [Authorize(Roles = "Patient")]
        [HttpPost("BookAnAppointment")]
        public async Task<IActionResult> PostAppointment(Appointments appointment)
        {
            try
            {
                var result = await _appointmentAdminService.AddAppointment(appointment);
                return Ok(result);
            }
            catch (InvalidAppointmentDateTimeException)
            {
                return BadRequest("Invalid appointment date. Please choose a future date.");
            }
            catch (ConflictingAppointmentsException)
            {
                return Conflict("The doctor already has another appointment around this time.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Unable to book appointment. Please try again later.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateDoctorIdInAppointments")]
        public async Task<IActionResult> UpdateAppointmentDoctor(AppointmentDoctorDTO appointmentDTO)
        {
            try
            {
                var appointment = await _appointmentAdminService.UpdateAppointmentDoctor(appointmentDTO.Id, appointmentDTO.DoctorId);
                if (appointment == null)
                    return NotFound("Appointment not found.");
                return Ok(appointment);
            }
            catch (Exception)
            {
                return BadRequest("Could not update appointment doctor.");
            }
        }

        [Authorize]
        [HttpPut("RescheduleAppointment")]
        public async Task<IActionResult> UpdateAppointmentDate(AppointmentDateDTO appointmentDTO)
        {
            try
            {
                var appointment = await _appointmentAdminService.UpdateAppointmentDate(appointmentDTO.Id, appointmentDTO.AppointmentDate);
                if (appointment == null)
                    return NotFound("Appointment not found or invalid date.");
                return Ok(appointment);
            }
            catch (Exception)
            {
                return BadRequest("Unable to reschedule appointment.");
            }
        }

        [Authorize]
        [HttpPut("StatusToCancelAppointment")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            try
            {
                var appointment = await _appointmentAdminService.CancelAppointment(id);
                if (appointment == null)
                    return NotFound("Appointment not found.");
                return Ok(appointment);
            }
            catch (Exception)
            {
                return BadRequest("Could not cancel the appointment.");
            }
        }

        [Authorize]
        [HttpPut("StatusToRescheduleAppointment")]
        public async Task<IActionResult> RescheduleAppointment(int id)
        {
            try
            {
                var appointment = await _appointmentAdminService.RescheduleAppointment(id);
                if (appointment == null)
                    return NotFound("Appointment not found.");
                return Ok(appointment);
            }
            catch (Exception)
            {
                return BadRequest("Could not reschedule appointment.");
            }
        }

        [Authorize]
        [HttpPut("StatusToCompleteAppointment")]
        public async Task<IActionResult> CompleteAppointment(int id)
        {
            try
            {
                var appointment = await _appointmentAdminService.CompleteAppointment(id);
                if (appointment == null)
                    return NotFound("Appointment not found.");
                return Ok(appointment);
            }
            catch (Exception)
            {
                return BadRequest("Could not complete appointment.");
            }
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet("ViewAppointmentsByDoctorId")]
        public async Task<IActionResult> GetAppointmentByDoctor(int doctorId)
        {
            try
            {
                var appointments = await _appointmentAdminService.GetAppointmentByDoctor(doctorId);
                return Ok(appointments);
            }
            catch (Exception)
            {
                return BadRequest("Unable to fetch appointments for doctor.");
            }
        }

        [Authorize(Roles = "Admin,Patient")]
        [HttpGet("ViewAppointmentsByPatientId")]
        public async Task<IActionResult> GetAppointmentByPatient(int patientId)
        {
            try
            {
                var appointments = await _appointmentAdminService.GetAppointmentByPatient(patientId);
                return Ok(appointments);
            }
            catch (Exception)
            {
                return BadRequest("Unable to fetch appointments for patient.");
            }
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet("ViewAllUpcomingAppointments")]
        public async Task<IActionResult> GetUpcomingAppointments()
        {
            try
            {
                var appointments = await _appointmentAdminService.GetUpcomingAppointments();
                return Ok(appointments);
            }
            catch (Exception)
            {
                return BadRequest("Unable to fetch upcoming appointments.");
            }
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpPut("ChangeAppointmentStatus")]
        public async Task<IActionResult> UpdateAppointmentStatus(AppointmentStatusDTO appointmentDTO)
        {
            try
            {
                var appointment = await _appointmentAdminService.UpdateAppointmentStatus(appointmentDTO.Id, appointmentDTO.Status);
                if (appointment == null)
                    return NotFound("Appointment not found.");
                return Ok(appointment);
            }
            catch (Exception)
            {
                return BadRequest("Unable to change appointment status.");
            }
        }
    }
}
