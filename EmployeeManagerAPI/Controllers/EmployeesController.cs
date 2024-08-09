using API.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using Database.Models;
using Shared;

namespace EmployeeManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public EmployeesController(ILogger<EmployeesController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadEmployeeDto>>> GetEmployeesAsync()
        {
            _logger.LogInformation("Getting employees");
            var employees = await _dbContext.Employees.Include(e => e.Position).Select(
                e => new ReadEmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Surname = e.Surname,
                    BirthDate = e.BirthDate,
                    Position = new ReadPositionDto
                    {
                        Id = e.Position.Id,
                        Name = e.Position.Name
                    },
                }).ToListAsync();

            return Ok(employees);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadEmployeeDto>> GetEmployeeAsync(int id)
        {
            _logger.LogInformation($"Getting employee with id: {id}");

            var employee = await _dbContext.Employees.Include(e => e.Position).SingleOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound($"Employee with id: {id} does not exist in database");
            }

            var readEmployeeDto = new ReadEmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                BirthDate = employee.BirthDate,
                Position = new ReadPositionDto
                {
                    Id = employee.Position.Id,
                    Name = employee.Position.Name
                },
                IpAddress = employee.IpAddress,
                IpCountryCode = employee.IpCountryCode
            };

            return Ok(readEmployeeDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            _logger.LogInformation("Adding employee");

            var exists = await _dbContext.Employees.AnyAsync(e => e.Name == createEmployeeDto.Name &&
                                                                  e.Surname == createEmployeeDto.Surname &&
                                                                  e.BirthDate == createEmployeeDto.BirthDate);
            if (exists)
            {
                return Conflict("There is already an employee with these values in the database");
            }

            var employee = new Employee
            {
                Name = createEmployeeDto.Name,
                Surname = createEmployeeDto.Surname,
                BirthDate = createEmployeeDto.BirthDate,
                PositionId = createEmployeeDto.PositionId,
                IpAddress = createEmployeeDto.IpAddress,
                IpCountryCode = createEmployeeDto.IpCountryCode
            };

            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
 
            return Ok();
        }


        [HttpPost("Upload")]
        public async Task<IActionResult> UploadEmployeesAsync([FromBody] List<BulkCreateEmployeeDto> createEmployeeDtos)
        {
            _logger.LogInformation("Uploading employees");

            var employees = new List<Employee>();

            foreach (var dto in createEmployeeDtos)
            {
                var exists = await _dbContext.Employees
                    .AnyAsync(e => e.Name == dto.Name && e.Surname == dto.Surname && e.BirthDate == dto.BirthDate);

                if (exists)
                {
                    _logger.LogWarning($"Employee '{dto.Name} {dto.Surname}' with birth date {dto.BirthDate} already exists.");
                    continue;
                }

                var position = await _dbContext.Positions.SingleOrDefaultAsync(p => p.Name == dto.Position);
                if (position == null)
                {
                    position = new Position { Name = dto.Position };
                    _dbContext.Positions.Add(position);
                    await _dbContext.SaveChangesAsync();
                }

                var employee = new Employee
                {
                    Name = dto.Name,
                    Surname = dto.Surname,
                    BirthDate = dto.BirthDate,
                    IpAddress = dto.IpAddress,
                    IpCountryCode = dto.IpCountryCode,
                    PositionId = position.Id
                };

                employees.Add(employee);
            }

            if (employees.Count > 0)
            {
                _dbContext.Employees.AddRange(employees);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Added {employees.Count} new employees.");
            }
            else
            {
                _logger.LogInformation("No new employees to add.");
            }

            return Ok();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            _logger.LogInformation($"Deleting employee with id: {id}");

            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync(int id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            _logger.LogInformation($"Updating employee with id: {id}");

            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var existingEmployee = await _dbContext.Employees
                .AnyAsync(e => e.Name == updateEmployeeDto.Name &&
                               e.Surname == updateEmployeeDto.Surname &&
                               e.BirthDate == updateEmployeeDto.BirthDate &&
                               e.Id != id);

            if (existingEmployee)
            {
                return Conflict("An employee with the same name, surname, and birthdate already exists.");
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Surname = updateEmployeeDto.Surname;
            employee.BirthDate = updateEmployeeDto.BirthDate;
            employee.PositionId = updateEmployeeDto.PositionId;
            employee.IpAddress = updateEmployeeDto.IpAddress;
            employee.IpCountryCode = updateEmployeeDto.IpCountryCode;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
