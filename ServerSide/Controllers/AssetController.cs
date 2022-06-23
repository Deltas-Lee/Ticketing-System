using _TicketSystem.Core.IConfiguration;
using _TicketSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class AssetController : ControllerBase
    {
        private readonly ILogger<AssetController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public AssetController(
            ILogger<AssetController> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Assets = await _unitOfWork.Assets.All();
            return Ok(Assets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var item = await _unitOfWork.Assets.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsset(Asset asset)
        {
            if (ModelState.IsValid)
            {
                /*var userId = ((ClaimsIdentity)User.Identity).FindFirst("Id").Value;
                var user = await _userManager.FindByIdAsync(userId);
                asset.UserFKId = user.Id;*/

                asset.Id = Guid.NewGuid();

                await _unitOfWork.Assets.Add(asset);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { asset.Id }, asset);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, Asset asset)
        {
            if (id != asset.Id)
                return BadRequest();

            await _unitOfWork.Assets.Upsert(asset);
            await _unitOfWork.CompleteAsync();

            // Following up the REST standart on update we need to return NoContent
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _unitOfWork.Assets.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Assets.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}



