using Microsoft.AspNetCore.Mvc;
using MVCproject.Repository;

namespace MVCproject.Controllers
{
    public class TestController : Controller
    {
        IDepartmentRepo DeptRepo;

        public TestController(IDepartmentRepo _DeptRepo)
        {
            DeptRepo = _DeptRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowServices([FromServices] IDepartmentRepo deptrepo)
        {

            return Content($"{DeptRepo.GetHashCode().ToString()} from constructor \n {deptrepo.GetHashCode().ToString()} from method");
        }
    }
}
