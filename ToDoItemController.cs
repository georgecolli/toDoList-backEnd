using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


[ApiController]
[Route("ToDoList")]
public class ToDoItemController : ControllerBase
{
    private readonly IRepository<ToDoItem> _toDoItemRepository;

    public ToDoItemController(IRepository<ToDoItem> toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<ToDoItem>> GetAll()
    {
        return await _toDoItemRepository.GetAll();
    }

    //?
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        try
        {
            var result = await _toDoItemRepository.Get(id);
            return Ok(result);
        }
        catch (Exception)
        {
            return NotFound($"No post with id {id} found!");
        }

    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            await _toDoItemRepository.Delete(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }

    }


    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ToDoItem toDoItem)
    {
        try
        {
            var newToDoItem = await _toDoItemRepository.Insert(toDoItem);
            return Created($"/Posts/{toDoItem.Id}", newToDoItem);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}
