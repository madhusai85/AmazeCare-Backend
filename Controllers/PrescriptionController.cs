using AmazeCare.Interfaces;
using AmazeCare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace AmazeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [Authorize(Roles = "Admin,Patient")]
        [HttpGet("ViewAllPrescriptions")]
        public async Task<ActionResult<List<Prescriptions>>> GetPrescription()
        {
            try
            {
                var prescriptions = await _prescriptionService.GetPrescriptionList();
                return Ok(prescriptions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Unable to fetch prescriptions.", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost("AddPrescription")]
        public async Task<ActionResult<Prescriptions>> PostMedicalRecord(Prescriptions prescriptions)
        {
            try
            {
                var created = await _prescriptionService.AddPrescription(prescriptions);
                return Ok(created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Unable to add prescription.", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Doctor")]
        [HttpPut("UpdateWholePrescription")]
        public async Task<ActionResult<Prescriptions>> UpdatePrescription(Prescriptions prescriptions)
        {
            try
            {
                var updated = await _prescriptionService.UpdatePrescription(prescriptions);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Unable to update prescription.", Error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("ViewPrescriptionByRecordId")]
        public async Task<ActionResult<List<Prescriptions>>> GetPrescriptionsByRecordId(int recordId)
        {
            try
            {
                var prescriptions = await _prescriptionService.GetPrescriptionsByRecordId(recordId);
                if (prescriptions == null || prescriptions.Count == 0)
                    return NotFound(new { Message = "No prescriptions found for this record ID." });

                return Ok(prescriptions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Unable to fetch prescriptions by record ID.", Error = ex.Message });
            }
        }
    }
}
