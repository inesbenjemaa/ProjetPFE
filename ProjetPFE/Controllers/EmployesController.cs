using Microsoft.AspNetCore.Mvc;
using ProjetPFE.Contracts.services;

namespace ProjetPFE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployesController : ControllerBase
    {
        private readonly IEmployeService employeService;
        public EmployesController(IEmployeService employeService)
        {
            this.employeService = employeService;
        }



        [HttpGet]
        public async Task<IActionResult> RetrieveAllEmploye()
        {
            var employes = await this.employeService.RetrieveEmployes();
            return Ok(employes);
        }
    }
}
