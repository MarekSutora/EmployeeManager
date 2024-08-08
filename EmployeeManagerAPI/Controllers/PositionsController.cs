using API.Database;
using Database.Models;
using EmployeeManagerAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PositionsController : ControllerBase
    {
        private readonly ILogger<PositionsController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public PositionsController(ILogger<PositionsController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadPositionDto>>> GetPositionsAsync()
        {
            try
            {
                var positions = await _dbContext.Positions.Select(p => new ReadPositionDto
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToListAsync();

                return Ok(positions);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadPositionsAsync(IEnumerable<CreatePositionDto> createPositionDtos)
        {
            try
            {
                var positions = createPositionDtos.Select(p => new Position
                {
                    Name = p.Name
                });

                await _dbContext.Positions.AddRangeAsync(positions);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
