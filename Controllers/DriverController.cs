namespace buildingLink.Controllers
{
    using buildingLink.Models;
    using buildingLink.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System;
     
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {

        private readonly IDriverRepository _driverRepo;
        private readonly ILogger<DriverController> _logger; 
        public DriverController(ILogger<DriverController> logger, IDriverRepository personRepo)
        {
            _logger = logger;
            _driverRepo = personRepo;
        }
 
        [HttpGet]
        public async Task<IActionResult> GetDrivers()
        {
            try
            {
                var people = await _driverRepo.GetDriversAsync();
                return Ok(people);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriver(Driver person)
        {
            try
            {
                var createdPerson = await _driverRepo.AddDriverAsync(person);
                return CreatedAtAction(nameof(CreateDriver), createdPerson);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDriver(Driver personToUpdate)
        {
            try
            {
                var person = await _driverRepo.GetDriverByIdAsync(personToUpdate.Id);
                if (person == null)
                {
                    return NotFound(new
                    {
                        statusCode = 404
                        ,
                        message = "Driver does not found"
                    });
                }
                await _driverRepo.UpdateDriverAsync(personToUpdate);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }





        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriverById(int id)
        {
            try
            {
                var person = await _driverRepo.GetDriverByIdAsync(id);
                if (person == null)
                {
                    return NotFound(new
                    {
                        statusCode = 404
                        ,
                        message = "Person does not found"
                    });
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            try
            {
                var person = await _driverRepo.GetDriverByIdAsync(id);
                if (person == null)
                {
                    return NotFound(new
                    {
                        statusCode = 404
                        ,
                        message = "Person does not found"
                    });
                }
                await _driverRepo.DeleteDriverAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

    }
}
