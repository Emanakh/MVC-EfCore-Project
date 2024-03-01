using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCproject.Models;
using MVCproject.Repository;
using System.Runtime.Intrinsics.X86;

namespace MVCproject.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {

        IDepartmentRepo DeptRepo;
        IStudentRepo StRepo;
        public StudentController(IStudentRepo _StRepo, IDepartmentRepo _DeptRepo)
        {
            StRepo = _StRepo;
            DeptRepo = _DeptRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Student> model = StRepo.GetAll();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.depts = DeptRepo.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student st, IFormFile stimg)
        {
            if (ModelState.IsValid && stimg != null && stimg.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stimg.CopyTo(ms);
                    st.Image = ms.ToArray();
                }
                StRepo.Add(st);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "please Douple check your data");
                ViewBag.depts = DeptRepo.GetAll();
                return View(st);

            }
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Student st = StRepo.GetById(id.Value);
            if (st == null)
            {
                return NotFound();
            }
            else
            {
                string imgdata = Convert.ToBase64String(st.Image);
                string imgUrl = string.Format("data:image/;base64,{0}", imgdata);
                ViewBag.imgUrl = imgUrl;
                return PartialView(st);
            }
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Student st = StRepo.GetById(id.Value);
            if (st == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.depts = DeptRepo.GetAll();
                return View(st);
            }
        }

        [HttpPost]

        public IActionResult Edit(Student st, IFormFile stimg)
        {
            Student old = StRepo.GetById(st.Id);
            if (stimg != null && stimg.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stimg.CopyTo(ms);
                    old.Image = ms.ToArray();
                }
            }
            StRepo.Update(old, st);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Student? st = StRepo.GetById(id.Value);
            if (st == null)
            {
                return NotFound();
            }
            else
            {
                StRepo.SoftDelete(id.Value);
                return RedirectToAction("Index");

            }
        }

        public IActionResult CheckEmail(string Email)
        {
            var st = StRepo.GetAll().FirstOrDefault(s => s.Email == Email);
            if (st != null)
            {
                return Json("there's an existing email with the same name");
            }
            else
            {
                return Json(true);
            }
        }

    }
}
