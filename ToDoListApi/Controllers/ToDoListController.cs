using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ToDoListApi.Database.AppDbContextModels;

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ToDoListController(AppDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            var lst = _db.ToDoLists.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var item = _db.ToDoLists.FirstOrDefault(x => x.TaskId == id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateTask(ToDoList requestModel)
        {
            _db.ToDoLists.Add(requestModel);
            _db.SaveChanges();

            return Ok(requestModel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, ToDoList requestModel)
        {
            var item = _db.ToDoLists.FirstOrDefault(x => x.TaskId == id);
            if (item is null)
                return BadRequest();

            item.TaskTitle = requestModel.TaskTitle;
            item.TaskDescription = requestModel.TaskDescription;
            item.PriorityLevel = requestModel.PriorityLevel;
            item.Status = requestModel.Status;
            item.DueDate = requestModel.DueDate;
            item.CreatedDate = requestModel.CreatedDate;
            item.CompletedDate = requestModel.CompletedDate;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var item = _db.ToDoLists.FirstOrDefault(x => x.TaskId == id);
            if (item == null)
                return NotFound();

            _db.ToDoLists.Remove(item);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
