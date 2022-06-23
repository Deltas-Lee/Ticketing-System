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
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public ClientController(
            ILogger<ClientController> logger,
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
            var Clients = await _unitOfWork.Clients.All();
            return Ok(Clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var item = await _unitOfWork.Clients.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(Client Client)
        {
            if (ModelState.IsValid)
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst("Id").Value;
                Client.userIdFk = userId;

                await _unitOfWork.Clients.Add(Client);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { Client.Id }, Client);
            }

            return new JsonResult("Something Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, Client Client)
        {
            if (id != Client.Id)
                return BadRequest();

            await _unitOfWork.Clients.Upsert(Client);
            await _unitOfWork.CompleteAsync();

            // Following up the REST standart on update we need to return NoContent
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _unitOfWork.Clients.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Clients.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}

