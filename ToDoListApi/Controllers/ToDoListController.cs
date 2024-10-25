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

        [HttpPost("sample-data")]
        public IActionResult SampleData()
        {
            var sampleTasks = new List<ToDoList>
            {
                new ToDoList { TaskTitle = "Complete Project Proposal", TaskDescription = "Prepare and submit the project proposal for client approval.", PriorityLevel = 5, Status = "Pending", DueDate = DateTime.Today.AddDays(3) },
                new ToDoList { TaskTitle = "Weekly Team Meeting", TaskDescription = "Discuss progress on current tasks and upcoming projects.", PriorityLevel = 3, Status = "Pending", DueDate = DateTime.Today.AddDays(1) },
                new ToDoList { TaskTitle = "Client Follow-up Call", TaskDescription = "Follow up with client regarding project status and requirements.", PriorityLevel = 4, Status = "Pending", DueDate = DateTime.Today.AddDays(2) },
                new ToDoList { TaskTitle = "Update Software Documentation", TaskDescription = "Revise and update the software documentation for the recent version release.", PriorityLevel = 2, Status = "In Progress", DueDate = DateTime.Today.AddDays(5) },
                new ToDoList { TaskTitle = "Conduct Code Review", TaskDescription = "Review code submitted by team members for accuracy and efficiency.", PriorityLevel = 4, Status = "In Progress", DueDate = DateTime.Today.AddDays(4) },
                new ToDoList { TaskTitle = "Prepare Financial Report", TaskDescription = "Generate and submit the monthly financial report for management review.", PriorityLevel = 5, Status = "Pending", DueDate = DateTime.Today.AddDays(6) },
                new ToDoList { TaskTitle = "HR Interview Schedule", TaskDescription = "Coordinate with HR to schedule interviews for the new hiring process.", PriorityLevel = 3, Status = "Completed", DueDate = DateTime.Today.AddDays(-1), CompletedDate = DateTime.Today },
                new ToDoList { TaskTitle = "Team Outing Planning", TaskDescription = "Plan logistics and activities for the upcoming team outing.", PriorityLevel = 1, Status = "Pending", DueDate = DateTime.Today.AddDays(10) },
                new ToDoList { TaskTitle = "Website Bug Fixes", TaskDescription = "Resolve reported bugs on the company website.", PriorityLevel = 5, Status = "In Progress", DueDate = DateTime.Today.AddDays(7) },
                new ToDoList { TaskTitle = "Prepare Presentation Slides", TaskDescription = "Design and finalize slides for the upcoming client presentation.", PriorityLevel = 4, Status = "Pending", DueDate = DateTime.Today.AddDays(3) }
            };

            _db.ToDoLists.AddRange(sampleTasks);
            _db.SaveChanges();

            return Ok("10 sample tasks have been inserted successfully.");
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
