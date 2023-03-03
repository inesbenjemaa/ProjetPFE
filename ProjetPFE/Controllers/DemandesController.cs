using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetPFE.Contracts;
using ProjetPFE.Dto;

namespace ProjetPFE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class DemandesController : ControllerBase
    {
        private readonly IDemandeRepository _demandeRepo;
        public DemandesController(IDemandeRepository demandeRepo)
        {
            _demandeRepo = demandeRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetDemandes()
        {
            try
            {
                var demandes = await _demandeRepo.Getdemandes();
                return Ok(demandes);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("{demande_id}", Name = "demandeById")]
        public async Task<IActionResult> GetDemande(int demande_id)
        {
            try
            {
                var demande = await _demandeRepo.GetDemande(demande_id);
                if (demande == null)
                    return NotFound();

                return Ok(demande);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateDemande(DemandeForCreationDto demande)
        {
            try
            {
                var createddemande = await _demandeRepo.CreateDemande(demande);
                return CreatedAtRoute("demandeById", new { demande_id = createddemande.demande_id }, createddemande);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{demande_id}")]
        public async Task<IActionResult> UpdateDemande(int demande_id, DemandeForUpdateDto demande)
        {
            try
            {
                var dbdemande = await _demandeRepo.GetDemande(demande_id);
                if (dbdemande == null)
                    return NotFound();

                await _demandeRepo.UpdateDemande(demande_id, demande);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }



        [HttpDelete("{demande_id}")]
        public async Task<IActionResult> DeleteDemande(int demande_id)
        {
            try
            {
                var dbdemande = await _demandeRepo.GetDemande(demande_id);
                if (dbdemande == null)
                    return NotFound();

                await _demandeRepo.DeleteDemande(demande_id);
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
    

