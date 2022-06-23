using _TicketSystem.Core.IConfiguration;
using _TicketSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _TicketSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusesController : ControllerBase
    {
        private readonly ILogger<StatusesController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public StatusesController(
            ILogger<StatusesController> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var statuses = await _unitOfWork.Statuses.All();
            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var item = await _unitOfWork.Statuses.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus(Status status)
        {
            if (ModelState.IsValid)
            {
                status.Id = Guid.NewGuid();

                await _unitOfWork.Statuses.Add(status);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { status.Id }, status);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, Status status)
        {
            if (id != status.Id)
                return BadRequest();

            await _unitOfWork.Statuses.Upsert(status);
            await _unitOfWork.CompleteAsync();

            // Following up the REST standart on update we need to return NoContent
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _unitOfWork.Statuses.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Statuses.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
