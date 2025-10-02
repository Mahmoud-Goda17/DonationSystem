using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationLogController(IServiceManger serviceManger) : ControllerBase
    {
        [HttpGet("GetAllNotificationLog")]
        public async Task<IActionResult> GetAllNotificationLogAsync()
        {
            var result = await serviceManger.NotificationLogService.GetAllNotificationLogAsync();
            return Ok(result);
        }
        [HttpGet("GetNotificationLogById/{id}")]
        public async Task<IActionResult> GetNotificationLogByIdAsync(int id)
        {
            var result = await serviceManger.NotificationLogService.GetNotificationLogByIdAsync(id);
            return Ok(result);
        }
        [HttpGet("GetAllNotificationLogByDonorId/{donorid}")]
        public async Task<IActionResult> GetAllNotificationLogByDonorIdAsync(int donorid)
        {
            var result = await serviceManger.NotificationLogService.GetAllNotificationLogByDonorIdAsync(donorid);
            return Ok(result);
        }
        [HttpGet("GetAllNotificationLogByRequestId/{requestid}")]
        public async Task<IActionResult> GetAllNotificationLogByRequestIdAsync(int requestid)
        {
            var result = await serviceManger.NotificationLogService.GetAllNotificationLogByRequestIdAsync(requestid);
            return Ok(result);
        }
    }
}
