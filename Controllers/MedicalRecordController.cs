using AmazeCare.Interfaces;
using AmazeCare.Models;
using AmazeCare.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace AmazeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly ILogger<MedicalRecordController> _logger;

        public MedicalRecordController(IMedicalRecordService medicalRecordService,
                                       ILogger<MedicalRecordController> logger)
        {
            _medicalRecordService = medicalRecordService;
            _logger = logger;
        }

        [Authorize]
        [Route("/ViewAllMedicalRecords")]
        [HttpGet]
        public async Task<IActionResult> GetMedicalRecord()
        {
            try
            {
                var medicalRecords = await _medicalRecordService.GetMedicalRecordList();
                return Ok(medicalRecords);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching medical records: {ex.Message}");
                return BadRequest(new { message = "Unable to fetch medical records." });
            }
        }

        [Authorize]
        [Route("/ViewMedicalRecordById")]
        [HttpGet]
        public async Task<IActionResult> GetMedicalRecordById(int id)
        {
            try
            {
                var medicalRecord = await _medicalRecordService.GetMedicalRecordById(id);
                return Ok(medicalRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching medical record {id}: {ex.Message}");
                return NotFound(new { message = $"Medical record with ID {id} not found." });
            }
        }

        [Authorize(Roles = "Doctor")]
        [Route("AddMedicalRecord")]
        [HttpPost]
        public async Task<IActionResult> PostMedicalRecord(MedicalRecords medicalRecords)
        {
            try
            {
                var result = await _medicalRecordService.AddMedicalRecord(medicalRecords);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding medical record: {ex.Message}");
                return BadRequest(new { message = "Unable to add medical record." });
            }
        }

        [Authorize]
        [Route("/ViewMedicalRecordByAppointmentId")]
        [HttpGet]
        public async Task<IActionResult> GetMedicalRecordByAppointment(int Id)
        {
            try
            {
                var result = await _medicalRecordService.GetMedicalRecordByAppointment(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching medical records by appointment {Id}: {ex.Message}");
                return BadRequest(new { message = "Unable to fetch medical records for this appointment." });
            }
        }

        [Authorize]
        [Route("/ViewAllMedicalRecordsByPatientId")]
        [HttpGet]
        public async Task<IActionResult> GetMedicalRecordByPatientId(int Id)
        {
            try
            {
                var result = await _medicalRecordService.GetMedicalRecordByPatientId(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching medical records by patient {Id}: {ex.Message}");
                return BadRequest(new { message = "Unable to fetch medical records for this patient." });
            }
        }

        [Authorize]
        [Route("/ViewAllMedicalRecordsByDoctorId")]
        [HttpGet]
        public async Task<IActionResult> GetMedicalRecordByDoctorId(int doctorId)
        {
            try
            {
                var result = await _medicalRecordService.GetMedicalRecordByDoctorId(doctorId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching medical records by doctor {doctorId}: {ex.Message}");
                return BadRequest(new { message = "Unable to fetch medical records for this doctor." });
            }
        }
    }
}
