#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Dtos;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly IMapper _mapper;

        public TodoItemsController(TodoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TodoItems
        /// <summary>
        /// Get all TodoItems Async
        /// </summary>
        /// <returns>Collection of TodoItems</returns>
        /// <response code="200">Returns all TodoItems</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoItemReadDto>>> GetAsync()
        {
            var todoItemsAsync = await _context.TodoItems.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<TodoItemReadDto>>(todoItemsAsync));
        }

        // GET: api/TodoItems/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
        /// <summary>
        /// Get TodoItem by Id Async
        /// </summary>
        /// <param name="id">Todo Id</param>
        /// <returns>TodoItem</returns>
        /// <response code="200">Returns TodoItem</response>
        /// <response code="404">TodoItem not found</response>
        /// <remarks>
        /// Get request:
        ///
        ///     GET api/TodoItems/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
        ///     {
        ///        "id": xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx,
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItemReadDto>> GetByIdAsync(Guid id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null) return NotFound();

            return Ok(_mapper.Map<TodoItemReadDto>(todoItem));
        }

        // PUT: api/TodoItems/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update TodoItem Async
        /// </summary>
        /// <param name="id">TodoItem Id to update</param>
        /// <param name="todoItemUpdateDto">TodoItem values - (Id is not authorized to change)</param>
        /// <returns></returns>
        /// <response code="204">Update passed successfully</response>
        /// <response code="400">The provided `Id` and `todoItem.Id` are not equal</response>
        /// <response code="422">TodoItem is invalid</response>
        /// <response code="404">TodoItem not found</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync(Guid id, TodoItemUpdateDto todoItemUpdateDto)
        {
            if (id != todoItemUpdateDto.Id) return BadRequest();

            var todoItem = _mapper.Map<TodoItem>(todoItemUpdateDto);

            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create TodoItem Async
        /// </summary>
        /// <param name="todoItemCreateDto">TodoItem to create</param>
        /// <returns>The new TodoItem</returns>
        /// <remarks>
        /// POST request:
        ///
        ///     POST api/TodoItems/
        ///     {
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Creation passed successfully</response>
        /// <response code="422">TodoItem is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<TodoItemCreateDto>> PostAsync(TodoItemCreateDto todoItemCreateDto)
        {
            var todoItem = _mapper.Map<TodoItem>(todoItemCreateDto);

            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetByIdAsync", new { id = todoItem.Id }, todoItem); // Still not working
        }

        // DELETE: api/TodoItems/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx,
        /// <summary>
        /// Delete TodoItem Async (Hard delete)
        /// </summary>
        /// <param name="id">TodoItem Id to delete</param>
        /// <returns></returns>
        /// <remarks>Hard deletion</remarks>
        /// <response code="204">Deletion passed successfully</response>
        /// <response code="404">TodoItem not found</response>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) return NotFound();

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        #region private methods
        /// <summary>
        /// Check TodoItem existence by its Id
        /// </summary>
        /// <param name="id"> TodoItem Id</param>
        /// <returns>bool</returns>
        private bool IsExist(Guid id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
        #endregion private methods
    }
}
