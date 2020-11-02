using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slb.Bootcamp.Service.ToDoList.Models;

namespace Slb.Bootcamp.Service.ToDoList.Controllers
{
    [ApiController]
    [Route("api/ToDoList")]
    public class ToDoItemController : ControllerBase
    {
        private IRepository _repository;
        private OperationRecord _record;

        public ToDoItemController(IRepository repository, OperationRecord record)
        {
            _repository = repository;
            _record = record;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDoItem>>> QueryAsync(
            string description, bool? done, [FromServices] OperationRecord record)
        {
            var list = await _repository.QueryAsync(description, done);
            return Ok(list);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ToDoItem>> UpdateAsync(
            string id,
            ToDoItemUpdateModel updateModel)
        {
            //Check Id
            if (string.IsNullOrEmpty(id))
                return BadRequest(new Dictionary<string, string>() { { "message", "Id is required" } });

            var modelInDb = await _repository.GetAsync(id);
            if (modelInDb == null)
                return NotFound(new Dictionary<string, string>() { { "message", $"Can't find {id}" } });

            //Update
            var updated = await _repository.UpdateAsync(id, updateModel);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoItem>> DeleteAsync(string id)
        {
            //Check Id
            if (string.IsNullOrEmpty(id))
                return BadRequest(new Dictionary<string, string>() { { "message", "Id is required" } });

            var modelInDb = await _repository.GetAsync(id);
            if (modelInDb == null)
                return NotFound(new Dictionary<string, string>() { { "message", $"Can't find {id}" } });

            //Delete
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<ToDoItem>> AddAsync(ToDoItem newItem)
        {
            //Check Id
            if (string.IsNullOrEmpty(newItem.Id))
                return BadRequest(new Dictionary<string, string>() { { "message", "Id is required" } });

            //Upsert
            await _repository.UpsertAsync(newItem);
            return Ok();
        }
    }
}
