using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetPFE.Contracts;
using ProjetPFE.Dto;

namespace ProjetPFE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private readonly IRoleRepository _roleRepo;


        public RolesController(IRoleRepository roleRepo)
        {
            _roleRepo = roleRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles = await _roleRepo.GetRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{role_id}", Name = "roleById")]
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
