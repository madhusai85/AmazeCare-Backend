using AmazeCare.Interfaces;
using AmazeCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AmazeCare.Models.DTOs;
using Microsoft.AspNetCore.Cors;

namespace AmazeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientAdminService _adminService;
        private readonly IPatientUserService _userService;

        public PatientController(IPatientAdminService adminService, IPatientUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("ViewAllPatients")]
        public async Task<ActionResult<List<Patients>>> GetPatient()
        {
            try
            {
                var patients = await _adminService.GetPatientList();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Unable to fetch patients.", Error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("ViewPatientById")]
        public async Task<ActionResult<Patients>> GetPatientById(int id)
        {
            try
            {
                var patient = await _userService.GetPatient(id);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = "Invalid patient ID.", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Patient,Admin")]
        [HttpPut("UpdatePatientAge")]
        public async Task<ActionResult<Patients>> UpdatePatientAge(PatientAgeDTO patientDTO)
        {
            try
            {
                var patient = await _adminService.UpdatePatientAge(patientDTO.Id, patientDTO.Age);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Unable to update age.", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Patient,Admin")]
        [HttpPut("UpdateMobileNumber")]
        public async Task<ActionResult<Patients>> UpdatePatientMobile(PatientMobileDTO patientDTO)
        {
            try
            {
                var patient = await _adminService.UpdatePatientMobile(patientDTO.Id, patientDTO.Mobile);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Unable to update mobile number.", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Patient,Admin")]
        [HttpDelete("DeletePatient")]
        public async Task<ActionResult<Patients>> DeletePatient(int id)
        {
            try
            {
                var patient = await _adminService.DeletePatient(id);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Unable to delete patient.", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Patient,Admin")]
        [HttpPut("UpdateAllDetailsOfThePatient")]
        public async Task<ActionResult<Patients>> UpdatePatient(Patients patients)
        {
            try
            {
                var updatedPatient = await _adminService.UpdatePatient(patients);
                return Ok(updatedPatient);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Unable to update patient details.", Error = ex.Message });
            }
        }

        [HttpGet("GetPatientIdByUsername")]
        public async Task<ActionResult<int>> GetPatientIdByUsername(string username)
        {
            try
            {
                var patientId = await _adminService.GetPatientIdByUsername(username);
                if (patientId != -1)
                    return Ok(patientId);

                return NotFound(new { Message = "No patient found with this username." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Unable to fetch patient ID.", Error = ex.Message });
            }
        }
    }
}
