using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DonationHistoryDtos;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationHistoryController(IServiceManger service) : ControllerBase
    {
        [HttpGet("GetAllDonationHistory")]
        public async Task<IActionResult> GetAllDonationHistory()
        {
            var donationhistories = await service.DonationHistoryService.GetAllDonationHistoryAsync();
            return Ok(donationhistories);
        }
        [HttpGet("GetDonationHistoryById/{id}")]
        public async Task<IActionResult> GetDonationHistoryById(int id)
        {
            var donationhistory = await service.DonationHistoryService.GetDonationHistoryById(id);
            return Ok(donationhistory);
        }
        [HttpPost("AddDonationHistory")]
        public async Task<IActionResult> AddDonationHistory([FromBody] CreateDonationHistoryDto createDonationHistoryDto)
        {
            var result = await service.DonationHistoryService.AddDonationHistory(createDonationHistoryDto);
            if (result <= 0) return BadRequest(new { Error = "Failed to add DonationHistory" });
            return Ok(new { Message = "DonationHistory added successfully" });
        }
        [HttpPut("UpdateDonationHistory")]
        public async Task<IActionResult> UpdateDonationHistory([FromBody] UpdateDonationHistoryDto updateDonationHistoryDto)
        {
            var result = await service.DonationHistoryService.UpdateDonationHistory(updateDonationHistoryDto);
            if (result <= 0) return BadRequest(new { Error = "Failed to update DonationHistory" });
            return NoContent();
        }
        [HttpDelete("DeleteDonationHistory/{id}")]
        public async Task<IActionResult> DeleteDonationHistory(int id)
        {
            var result = await service.DonationHistoryService.DeleteDonationHistoryAsync(id);
            if (result <= 0) return BadRequest(new { Error = "Failed to delete DonationHistory" });
            return NoContent();
        }
    }
}
