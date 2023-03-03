using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetPFE.Contracts;
using ProjetPFE.Dto;

namespace ProjetPFE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationsController : ControllerBase
    {
        private readonly IValidationRepository _validationRepo;


        public ValidationsController(IValidationRepository validationRepo)
        {
            _validationRepo = validationRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetValidations()
        {
            try
            {
                var validation = await _validationRepo.GetValidations();
                return Ok(validation);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{validation_id}", Name = "validationById")]
        public async Task<IActionResult> GetValidation(int validation_id)
        {
            try
            {
                var validation = await _validationRepo.GetValidation(validation_id);
                if (validation == null)
                    return NotFound();

                return Ok(validation);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateValidation(ValidationForCreationDto validation)
        {
            try
            {
                var createdvalidation = await _validationRepo.CreateValidation(validation);
                return CreatedAtRoute("ValidationById", new { validation_id = createdvalidation.validation_id }, createdvalidation);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{statut_id}")]
        public async Task<IActionResult> UpdateValidation(int validation_id, ValidationForUpdateDto validation)
        {
            try
            {
                var dbvalidation = await _validationRepo.GetValidation(validation_id);
                if (dbvalidation == null)
                    return NotFound();

                await _validationRepo.UpdateValidation(validation_id, validation);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }



        [HttpDelete("{validation_id}")]
        public async Task<IActionResult> Deletevalidation(int validation_id)
        {
            try
            {
                var dbvalidation = await _validationRepo.GetValidation(validation_id);
                if (dbvalidation == null)
                    return NotFound();

                await _validationRepo.DeleteValidation(validation_id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
