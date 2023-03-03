using Dapper;
using Microsoft.AspNetCore.Mvc;
using ProjetPFE.Contracts;
using ProjetPFE.Dto;
using ProjetPFE.Entities;
using System.Data;

namespace ProjetPFE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeursController : ControllerBase
    {
        private readonly IDemandeurRepository _demandeurRepo;

        public DemandeursController(IDemandeurRepository demandeurRepo)
        {
            _demandeurRepo = demandeurRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetDemandeurs()
        {
            try
            {
                var demandeurs = await _demandeurRepo.Getdemandeurs();
                return Ok(demandeurs);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{demandeur_id}", Name = "demandeurById")]
        public async Task<IActionResult> GetDemandeur(int demandeur_id)
        {
            try
            {
                var demandeur = await _demandeurRepo.GetDemandeur(demandeur_id);
                if (demandeur == null)
                    return NotFound();

                return Ok(demandeur);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateDemandeur(DemandeurForCreationDto demandeur)
        {
            try
            {
                var createddemandeur = await _demandeurRepo.CreateDemandeur(demandeur);
                return CreatedAtRoute("demandeurById", new { demandeur_id = createddemandeur.demandeur_id }, createddemandeur);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{demandeur_id}")]
        public async Task<IActionResult> UpdateDemandeur(int demandeur_id, DemandeurForUpdateDto demandeur)
        {
            try
            {
                var dbdemandeur = await _demandeurRepo.GetDemandeur(demandeur_id);
                if (dbdemandeur == null)
                    return NotFound();

                await _demandeurRepo.UpdateDemandeur(demandeur_id, demandeur);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{demandeur_id}")]
        public async Task<IActionResult> DeleteDemandeur(int demandeur_id)
        {
            try
            {
                var dbdemandeur = await _demandeurRepo.GetDemandeur(demandeur_id);
                if (dbdemandeur == null)
                    return NotFound();

                await _demandeurRepo.DeleteDemandeur(demandeur_id);
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
