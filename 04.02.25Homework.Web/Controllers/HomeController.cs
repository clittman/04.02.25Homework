using _04._02._25Homework.Data;
using _04._02._25Homework.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _04._02._25Homework.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=true;TrustServerCertificate=yes;";

        public IActionResult Index()
        {
            PeopleDb db = new(_connectionString);
            PeopleViewModel vm = new()
            {
                People = db.GetPeople()
            };

            if (TempData["message"] != null)
            {
                vm.Message = (string)TempData["message"];
            }

            return View(vm);
        }

        public IActionResult AddPeople()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPeople(List<Person> people)
        {
            PeopleDb db = new(_connectionString);
            db.AddPeople(people);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult DeleteMany(List<int> ids)
        {
            PeopleDb db = new(_connectionString);
            db.DeleteMany(ids);

            string text = "person";
            if(ids.Count > 1)
            {
                text = "people";
            }
            TempData["message"] = $"{ids.Count} {text} deleted successfully";
            return Redirect("/");
        }
    }
}
