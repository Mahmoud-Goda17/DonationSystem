using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DonationRequestDtos;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationRequestController(IServiceManger serviceManger) : ControllerBase
    {
        [HttpGet("GetAllDonationRequests")]
        public async Task<IActionResult> GetAllDonationRequests()
        {
            var donationRequests = await serviceManger.DonationRequestService.GetAllDonationRequestAsync();
            return Ok(donationRequests);
        }
        [HttpGet("GetDonationRequestById/{id}")]
        public async Task<IActionResult> GetDonationRequestById(int id)
        {
            var donationRequest = await serviceManger.DonationRequestService.GetDonationRequestByIdAsync(id);
            return Ok(donationRequest);
        }
        [HttpPost("AddDonationRequest")]
        public async Task<IActionResult> AddDonationRequest([FromBody] CreateDonationRequestDto createDonationRequestDto)
        {
            var result = await serviceManger.DonationRequestService.AddDonationRequestAsync(createDonationRequestDto);
            if (result <= 0) return BadRequest(new { Error = "Failed to add DonationRequest" });
            return Ok(new { Message = "DonationRequest added successfully" });
        }
        [HttpPut("UpdateDonationRequest/{id}")]
        public async Task<IActionResult> UpdateDonationRequest(int id, [FromBody] UpdateDonationRequestDto updateDonationRequestDto)
        {
            if (id != updateDonationRequestDto.Id)
                return BadRequest(new { Error = "Id mismatch" });
            var result = await serviceManger.DonationRequestService.UpdateDonationRequestAsync(updateDonationRequestDto);
            if (result <= 0) return BadRequest(new { Error = "Failed to update DonationRequest" });
            return NoContent();
        }
        [HttpDelete("DeleteDonationRequest/{id}")]
        public async Task<IActionResult> DeleteDonationRequest(int id)
        {
            var result = await serviceManger.DonationRequestService.DeleteDonationRequestAsync(id);
            if (result <= 0) return BadRequest(new { Error = "Failed to delete DonationRequest" });
            return NoContent();
        }
        [HttpGet("GetDonationRequestsByUrgent/{urgent}")]
        public async Task<IActionResult> GetDonationRequestsByUrgent(bool urgent)
        {
            var donationRequests = await serviceManger.DonationRequestService.GetDonationRequestByUrgent(urgent);
            return Ok(donationRequests);
        }
        [HttpGet("GetDonationRequestsByBloodType/{type}")]
        public async Task<IActionResult> GetDonationRequestsByBloodType(BloodTypes type)
        {
            var donationRequests = await serviceManger.DonationRequestService.GetDonationRequestByBloodType(type);
            return Ok(donationRequests);
        }
        [HttpGet("GetDonationRequestsByHospitalName/{hospitalName}")]
        public async Task<IActionResult> GetDonationRequestsByHospitalName(string hospitalName)
        {
            var donationRequests = await serviceManger.DonationRequestService.GetDonationRequesrByHospitalName(hospitalName);
            return Ok(donationRequests);
        }
        [HttpGet("GetPendingRequests")]
        public async Task<IActionResult> GetPendingRequests()
        {
            var donationRequests = await serviceManger.DonationRequestService.GetPendingRequest();
            return Ok(donationRequests);
        }
        [HttpPatch("CompleteRequest/{id}")]
        public async Task<IActionResult> CompleteRequest(int id)
        {
            var result = await serviceManger.DonationRequestService.CompleteRequestAsync(id);
            if (result <= 0) return BadRequest(new { Error = "Failed to complete DonationRequest" });
            return Ok(new { Message = "DonationRequest completed successfully" });
        }
        [HttpPatch("CancelRequest/{id}")]
        public async Task<IActionResult> CancelRequest(int id)
        {
            var result = await serviceManger.DonationRequestService.CancelRequestAsync(id);
            if (result <= 0) return BadRequest(new { Error = "Failed to cancel DonationRequest" });
            return Ok(new { Message = "DonationRequest canceled successfully" });
        }
    }
}
