using _TicketSystem.Core.IConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _TicketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace _TicketSystem.Controllers
{ 

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BillingController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly ILogger<BillingController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public BillingController(
               ILogger<BillingController> logger,
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
            var billing = await _unitOfWork.Billings.All();
            return Ok(billing);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var item = await _unitOfWork.Billings.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Createbill(Billing billing)
        {
            if (ModelState.IsValid)
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst("Id").Value;
                var user = await _userManager.FindByIdAsync(userId);

                billing.UserFKId = user.Id;
                billing.Id = Guid.NewGuid();

                await _unitOfWork.Billings.Add(billing);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { billing.Id }, billing);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, Billing billing)
        {
            if (id != billing.Id)
                return BadRequest();

            await _unitOfWork.Billings.Upsert(billing);
            await _unitOfWork.CompleteAsync();

            // Following up the REST standart on update we need to return NoContent
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _unitOfWork.Billings.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Billings.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }

    }
}
