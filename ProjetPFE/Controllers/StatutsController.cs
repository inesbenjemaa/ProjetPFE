using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetPFE.Contracts;
using ProjetPFE.Dto;
using ProjetPFE.Repository;


namespace ProjetPFE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatutsController : ControllerBase
    {
        private readonly IStatutRepository _statutRepo;


        public StatutsController(IStatutRepository statutRepo)
        {
            _statutRepo = statutRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetStatuts()
        {
            try
            {
                var statuts = await _statutRepo.GetStatuts();
                return Ok(statuts);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{statut_id}", Name = "statutById")]
        public async Task<IActionResult> GetStatut(int statut_id)
        {
            try
            {
                var statut = await _statutRepo.GetStatut(statut_id);
                if (statut == null)
                    return NotFound();

                return Ok(statut);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateStatut(StatutForCreationDto statut)
        {
            try
            {
                var createdstatut = await _statutRepo.CreateStatut(statut);
                return CreatedAtRoute("StatutById", new { id = createdstatut.statut_id }, createdstatut);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{statut_id}")]
        public async Task<IActionResult> UpdateStatut(int statut_id, StatutForUpdateDto statut)
        {
            try
            {
                var dbstatut = await _statutRepo.GetStatut(statut_id);
                if (dbstatut == null)
                    return NotFound();

                await _statutRepo.UpdateStatut(statut_id, statut);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }



        [HttpDelete("{statut_id}")]
        public async Task<IActionResult> DeleteStatut(int statut_id)
        {
            try
            {
                var dbstatut = await _statutRepo.GetStatut(statut_id);
                if (dbstatut == null)
                    return NotFound();

                await _statutRepo.DeleteStatut(statut_id);
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
