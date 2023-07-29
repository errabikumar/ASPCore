using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Task.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Data;
using NuGet.Protocol;

//changes001
namespace MVC_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult EnrollStudent(Section s=null )
        {
            Guid obj = Guid.NewGuid();
            s.Id = Convert.ToString(obj);
            List<Course> CourseList = new List<Course>();
            DataTable dt = new DataLayer().GetCourse();
            CourseList = (from DataRow dr in dt.Rows
                          select new Course()
                           {
                               Id = Convert.ToString(dr["Id"]),
                               Name = Convert.ToString(dr["Name"])
                          }).ToList();

           
            ViewBag.CourseList = new SelectList(CourseList, "Id", "Name");

            if (ModelState.IsValid)
            {
                if (s != null)
                {
                    if (s.Order==0)
                    {
                        ModelState.AddModelError("Order", "Order should not be zero");

                        return View();
                    }
                    if (s.Name.Trim() == "")
                    {
                        ModelState.AddModelError("Name", "Name should not be blank");

                        return View();
                    }
                    if (s.CourseId =="")
                    {
                        ModelState.AddModelError("CourseId", "Please select course");

                        return View();
                    }

                    string msg= new DataLayer().InsertData(s);
                    if (msg.Contains("successfully"))
                    {
                        TempData["msg"] = msg;
                        return RedirectToAction("SectionList");
                    }
                    else
                    {
                        ModelState.AddModelError("Name", msg);

                        return View();
                    }
                }
            }
           
            return View();
        }
        public IActionResult SectionList()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetSectionList()
        {
            List<Course> CourseList = new List<Course>();
            DataTable dt = new DataLayer().GetSectionList();

            return Json(dt.ToJson());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}