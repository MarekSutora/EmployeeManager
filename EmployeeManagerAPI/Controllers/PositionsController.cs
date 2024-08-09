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
            _logger.LogInformation("Getting positions");

            var positions = await _dbContext.Positions.Select(p => new ReadPositionDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToListAsync();

            return Ok(positions);

        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadPositionsAsync(IEnumerable<CreatePositionDto> createPositionDtos)
        {
            _logger.LogInformation("Uploading positions");

            var existingPositionNames = await _dbContext.Positions
                .AsNoTracking()
                .Select(p => p.Name)
                .ToListAsync();

            var newPositions = createPositionDtos
                .Where(p => !existingPositionNames.Contains(p.Name))
                .Select(p => new Position
                {
                    Name = p.Name
                })
                .ToList();

            if (newPositions.Count > 0)
            {
                await _dbContext.Positions.AddRangeAsync(newPositions);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Added {newPositions.Count} new positions.");
            }
            else
            {
                _logger.LogInformation("No new positions to add.");
            }

            return Ok();
        }
    }
}
