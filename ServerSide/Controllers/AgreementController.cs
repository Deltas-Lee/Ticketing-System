using _TicketSystem.Core.IConfiguration;
using _TicketSystem.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _TicketSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AgreementController : ControllerBase
    {
        private readonly ILogger<AgreementController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public AgreementController(
            ILogger<AgreementController> logger,
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
            var Agreement = await _unitOfWork.Agreements.All();
            return Ok(Agreement);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var item = await _unitOfWork.Agreements.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Agreement ticket)
        {
            if (ModelState.IsValid)
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst("Id").Value;

                ticket.UserFKId = userId;

                await _unitOfWork.Agreements.Add(ticket);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { ticket.Id }, ticket);
            }

            return new JsonResult("Something Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, Agreement ticket)
        {
            if (id != ticket.Id)
                return BadRequest();

            await _unitOfWork.Agreements.Upsert(ticket);
            await _unitOfWork.CompleteAsync();

            // Following up the REST standart on update we need to return NoContent
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _unitOfWork.Agreements.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Agreements.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
