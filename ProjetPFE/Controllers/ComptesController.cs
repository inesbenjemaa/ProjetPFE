using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetPFE.Contracts;
using ProjetPFE.Dto;

namespace ProjetPFE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComptesController : ControllerBase
    {

        private readonly ICompteRepository _compteRepo;


        public ComptesController(ICompteRepository compteRepo)
        {
            _compteRepo = compteRepo;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetComptes()
        {
            try
            {
                var comptes = await _compteRepo.GetComptes();
                return Ok(comptes);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{compte_id}", Name = "compteById")]
        public async Task<IActionResult> GetCompte(int compte_id)
        {
            try
            {
                var compte = await _compteRepo.GetCompte(compte_id);
                if (compte == null)
                    return NotFound();

                return Ok(compte);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateCompte(CompteForCreationDto compte)
        {
            try
            {
                var createdcompte = await _compteRepo.CreateCompte(compte);
                return CreatedAtRoute("roleById", new { compte_id = createdcompte.compte_id }, createdcompte);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{compte_id}")]
        public async Task<IActionResult> UpdateCompte(int compte_id, CompteForUpdateDto compte)
        {
            try
            {
                var dbcompte = await _compteRepo.GetCompte(compte_id);
                if (dbcompte == null)
                    return NotFound();

                await _compteRepo.UpdateCompte(compte_id, compte);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }



        [HttpDelete("{compte_id}")]
        public async Task<IActionResult> DeleteCompte(int compte_id)
        {
            try
            {
                var dbcompte = await _compteRepo.GetCompte(compte_id);
                if (dbcompte == null)
                    return NotFound();

                await _compteRepo.DeleteCompte(compte_id);
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
