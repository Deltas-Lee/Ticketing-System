using _TicketSystem.Core.IConfiguration;
using _TicketSystem.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _TicketSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public TicketController(
            ILogger<TicketController> logger,
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Tickets = await _unitOfWork.Tickets.All();
            return Ok(Tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var item = await _unitOfWork.Tickets.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst("Id").Value;
                ticket.UserFKId = userId;

                await _unitOfWork.Tickets.Add(ticket);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { ticket.Id }, ticket);
            }

            return new JsonResult("Something Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, Ticket ticket)
        {
            if (id != ticket.Id)
                return BadRequest();

            await _unitOfWork.Tickets.Upsert(ticket);
            await _unitOfWork.CompleteAsync();

            // Following up the REST standart on update we need to return NoContent
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _unitOfWork.Tickets.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Tickets.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}

