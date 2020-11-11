using ConsoleApp1.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoItemsController : ControllerBase
    {
        private MemoryRepository _repository = MemoryRepository.Instance;

        [HttpGet]
        public async Task<ActionResult<List<ToDoItem>>> QueryAsync(
            string description, bool? done)
        {
            var list = await _repository.QueryAsync(description, done);
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ToDoItem>>> GetAsync(
            string id)
        {
            var list = await _repository.GetAsync(id);
            return Ok(list);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<ToDoItem>> UpdateAsync(
            [Required]string id,
            [Required]ToDoItemUpdateModel updateModel)
        {
            var modelInDb = await _repository.GetAsync(id);
            if (modelInDb == null)
                return NotFound(new Dictionary<string, string>() { { "message", $"Can't find {id}" } });

            //update
            var updated = await _repository.UpdateAsync(id, updateModel);
            return Ok(updated);
        }

        [HttpPut]
        public async Task<ActionResult<ToDoItem>> UpsertAsync(ToDoItem item)
        {
            //check id
            if (string.IsNullOrEmpty(item.Id))
                return BadRequest(new Dictionary<string, string>() { { "message", "Id is required" } });

            await _repository.UpsertAsync(item);

            var model = await _repository.GetAsync(item.Id);
            if (model == null)
                return new ObjectResult(500) { };

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(
            [Required] string id)
        {
            var modelInDb = await _repository.GetAsync(id);
            if (modelInDb == null)
                return NotFound(new Dictionary<string, string>() { { "message", $"Can't find {id}" } });

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
