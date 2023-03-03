using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetPFE.Contracts;
using ProjetPFE.Dto;
using ProjetPFE.Repository;

namespace ProjetPFE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivesController : ControllerBase
    {
        private readonly IArchiveRepository _archiveRepo;


        public ArchivesController(IArchiveRepository archiveRepo)
        {
            _archiveRepo = archiveRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetArchives()
        {
            try
            {
                var archives = await _archiveRepo.GetArchives();
                return Ok(archives);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{arch_id}", Name = "archiveById")]
        public async Task<IActionResult> GetArchive(int arch_id)
        {
            try
            {
                var archive = await _archiveRepo.GetArchive(arch_id);
                if (archive == null)
                    return NotFound();

                return Ok(archive);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateArchive(ArchiveForCreationDto archive)
        {
            try
            {
                var createdarchive = await _archiveRepo.CreateArchive(archive);
                return CreatedAtRoute("roleById", new { arch_id = createdarchive.arch_id }, createdarchive);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{arch_id}")]
        public async Task<IActionResult> UpdateArchive(int arch_id, ArchiveForUpdateDto archive)
        {
            try
            {
                var dbarchive = await _archiveRepo.GetArchive(arch_id);
                if (dbarchive == null)
                    return NotFound();

                await _archiveRepo.UpdateArchive(arch_id, archive);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }



        [HttpDelete("{arch_id}")]
        public async Task<IActionResult> DeleteArchive(int arch_id)
        {
            try
            {
                var dbcompte = await _archiveRepo.GetArchive(arch_id);
                if (dbcompte == null)
                    return NotFound();

                await _archiveRepo.DeleteArchive(arch_id);
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
