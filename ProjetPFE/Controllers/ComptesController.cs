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
        public async Task<IActionResult> GetRole(int role_id)
        {
            try
            {
                var role = await _roleRepo.GetRole(role_id);
                if (role == null)
                    return NotFound();

                return Ok(role);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleForCreationDto role)
        {
            try
            {
                var createdrole = await _roleRepo.CreateRole(role);
                return CreatedAtRoute("roleById", new { role_id = createdrole.role_id }, createdrole);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{role_id}")]
        public async Task<IActionResult> UpdateRole(int role_id, RoleForUpdateDto role)
        {
            try
            {
                var dbrole = await _roleRepo.GetRole(role_id);
                if (dbrole == null)
                    return NotFound();

                await _roleRepo.UpdateRole(role_id, role);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }



        [HttpDelete("{role_id}")]
        public async Task<IActionResult> DeleteRole(int role_id)
        {
            try
            {
                var dbrole = await _roleRepo.GetRole(role_id);
                if (dbrole == null)
                    return NotFound();

                await _roleRepo.DeleteRole(role_id);
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
