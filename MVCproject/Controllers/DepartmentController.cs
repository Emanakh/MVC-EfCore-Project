using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCproject.Models;
using MVCproject.Repository;

namespace MVCproject.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentRepo DeptRepo;
        public DepartmentController(IDepartmentRepo _DeptRepo)
        {
            DeptRepo = _DeptRepo;
        }
        public IActionResult Index()
        {
            var model = DeptRepo.GetAll();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department dept)
        {
            if (dept.DeptId != 0 && dept.DeptName != null)
            {
                DeptRepo.Add(dept);
                return RedirectToAction("Index");
            }
            else return View(dept);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Department dept = DeptRepo.GetById(id.Value);
            if (dept == null)
            {
                return NotFound();
            }
            else return PartialView(dept);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Department dept = DeptRepo.GetById(id.Value);

            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }

        [HttpPost]
        public IActionResult Edit(Department dept, int id)
        {
            DeptRepo.Update(dept, id);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {


            if (id == null)
            {
                return BadRequest();
            }
            Department dept = DeptRepo.GetById(id.Value);
            if (dept == null)
            {
                return NotFound();
            }
            else
            {
                DeptRepo.SoftDelete(id.Value);
                return RedirectToAction("Index");

            }
        }


    }
}
