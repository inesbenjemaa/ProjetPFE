using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetPFE.Contracts;
using ProjetPFE.Contracts.services;
using ProjetPFE.Dto;
using ProjetPFE.Entities;
using ProjetPFE.Repository;

namespace ProjetPFE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployesController : ControllerBase

    {


        private readonly IEmployeService employeService;
        private readonly IEmployeRepository _employeRepository;

        public EmployesController(IEmployeService employeService)
        {
            this.employeService = employeService;
        }



        [HttpGet]
        public async Task<ActionResult<ICollection<employe>>> RetrieveAllEmploye()
        {
            var employes = await this.employeService.RetrieveEmployes();
            return Ok(employes);
        }




        [HttpPost]
        public async Task<IActionResult> AddEmploye(employe employe)
        {
            int rowsAffected = await _employeRepository.AddEmployeAsync(employe);
            if (rowsAffected == 0)
            {
                return BadRequest("Unable to add employe.");
            }
            return Ok("Employe added successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmploye(int id, employe employe)
        {
            if (id != employe.employe_id)
            {
                return BadRequest("Employe ID mismatch.");
            }
            int rowsAffected = await _employeRepository.UpdateEmployeAsync(employe);
            if (rowsAffected == 0)
            {
                return BadRequest("Unable to update employe.");
            }
            return Ok("Employe updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmploye(int id)
        {
            var employe = await _employeRepository.GetEmployeByIdAsync(id);
            if (employe == null)
            {
                return NotFound();
            }

            await _employeRepository.DeleteEmployeAsync(id);

            return NoContent();
        }
    }
}
