using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DonorDtos;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonorController(IServiceManger service) : ControllerBase
    {
        [HttpGet("GetAllDonors")]
        public async Task<IActionResult> GetAllDonors()
        {
            var result = await service.DonorService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("GetDonorById/{id}")]
        public async Task<IActionResult> GetDonorById(int id)
        {
            var result = await service.DonorService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("CreateDonor")]
        public async Task<IActionResult> CreateDonor([FromBody] CreateDonorDto createDonorDto)
        {            
            var result = await service.DonorService.AddAsync(createDonorDto);
            if (result <= 0) return BadRequest(new { Error = "Failed to add Donor" });
            return Ok(new { Message = "Donor added successfully" });

        }
        [HttpPut("UpdateDonor")]
        public async Task<IActionResult> UpdateDonor(int id,[FromBody] UpdateDonorDto updateDonorDto)
        {
            if (id != updateDonorDto.Id)
                return BadRequest(new { Error = "Id mismatch" });
            var result = await service.DonorService.UpdateAsync(updateDonorDto);
            if (result <= 0) return BadRequest(new { Error = "Failed to update Donor" });
            return NoContent();
        }
        [HttpDelete("DeleteDonor/{id}")]
        public async Task<IActionResult> DeleteDonor(int id)
        {
            var result = await service.DonorService.DeleteAsync(id);
            if (result <= 0) return BadRequest(new { Error = "Failed to delete Donor" });
            return NoContent();
        }
        [HttpGet("GetDonorsByBloodType/{type}")]
        public async Task<IActionResult> GetDonorsByBloodType(BloodTypes type)
        {
            var result = await service.DonorService.GetByBloodTypeAsync(type);
            return Ok(result);
        }
        [HttpGet("GetAvailableDonors")]
        public async Task<IActionResult> GetAvailableDonors()
        {
            var result = await service.DonorService.GetAvalibaleDonorsAsync();
            return Ok(result);
        }
        [HttpGet("GetDonorsByLocation")]
        public async Task<IActionResult> GetDonorsByLocation([FromQuery] string address)
        {
            var result = await service.DonorService.GetDonorsByLocationAsync(address);
            return Ok(result);
        }
        [HttpGet("GetDonorsByEmail")]
        public async Task<IActionResult> GetDonorsByEmail([FromQuery] string email)
        {
            var result = await service.DonorService.GetDonorsByEmailAsync(email);
            return Ok(result);
        }
        [HttpGet("GetDonorWithHistory/{id}")]
        public async Task<IActionResult> GetDonorWithHistory(int id)
        {
            var result = await service.DonorService.GetDonorsWithHistory(id);
            return Ok(result);
        }
    }
}
