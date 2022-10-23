using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class StudentsController : Controller
    {
        private readonly DataContext _context;
        public StudentsController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Json(_context.Students.ToList());
        }
        [HttpGet]
        public ActionResult Details(long id = 0)
        {

            Student s = _context.Students.Find(id);

            return Json(s);
        }
        [HttpPost]
        public ActionResult Create(Student s)
        {

            _context.Students.Add(s);
            _context.SaveChanges();
            return Json(new { StatusCode = 201, message = "Data Created Successfully" });

        }

        [HttpPut]
        public ActionResult Edit(Student s)
        {
            _context.Students.Update(s);
            _context.SaveChanges();
            return Json(new { StatusCode = 204, message = "Data Updated Successfully" });
        }

        [HttpDelete]
  
        public ActionResult Delete(long id)
        {
            var s = _context.Students.Find(id);
            _context.Students.Remove(s);
            _context.SaveChanges();
            return Json(new { StatusCode = 204, message = "Data Deleted Successfully" });
        }




    }
}
