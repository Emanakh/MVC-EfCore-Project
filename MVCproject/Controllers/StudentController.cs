using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCproject.Models;
using MVCproject.Repository;
using System.Runtime.Intrinsics.X86;

namespace MVCproject.Controllers
{
    public class StudentController : Controller
    {
        AppDbContext db = new AppDbContext();

        // dept, student repo 
        IDepartmentRepo DeptRepo;
        IStudentRepo StRepo;
        public StudentController(IStudentRepo _StRepo, IDepartmentRepo _DeptRepo)
        {
            StRepo = _StRepo;
            DeptRepo = _DeptRepo;
        }

        public IActionResult Index()
        {
            //IEnumerable<Student> model = db.Students.Include(s => s.Department).ToList();
            IEnumerable<Student> model = StRepo.GetAll();
            return View(model);
        }

        //add student
        public IActionResult Create()
        {
            //IEnumerable<Department> depts = DeptRepo.GetAll();
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

                //db.Students.Add(st);
                //db.SaveChanges();
                StRepo.Add(st);
                return RedirectToAction("Index");
            }
            else
            {
                //IEnumerable<Department> depts = DeptRepo.GetAll();
                ViewBag.depts = DeptRepo.GetAll();
                return View(st);

            }
        }
        //details of student on page
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            //Student? st = db.Students.Include(f => f.Department).SingleOrDefault(d => d.Id == id);
            Student st = StRepo.GetById(id.Value);
            if (st == null)
            {
                return NotFound();
            }
            else
            {
                string imgdata =
Convert.ToBase64String(st.Image);
                string imgUrl =
            string.Format("data:image/;base64,{0}",
            imgdata);
                ViewBag.imgUrl = imgUrl;
                return PartialView(st);
            }
        }

        //edit a sudent
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            //Student? st = db.Students.SingleOrDefault(d => d.Id == id);
            Student st = StRepo.GetById(id.Value);
            if (st == null)
            {
                return NotFound();
            }
            else
            {
                //IEnumerable<Department> depts = DeptRepo.GetAll();
                ViewBag.depts = DeptRepo.GetAll(); ;



                return View(st);
            }
        }

        [HttpPost]

        public IActionResult Edit(Student st, IFormFile stimg)
        {

            //var old = db.Students.FirstOrDefault(d => d.Id == st.Id);
            Student old = StRepo.GetById(st.Id);
            if (stimg != null && stimg.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stimg.CopyTo(ms);
                    old.Image = ms.ToArray();
                }
            }
            //old.Age = st.Age;
            //old.Email = st.Email;
            //old.DeptNo = st.DeptNo;
            //old.Name = st.Name;
            //db.SaveChanges();
            StRepo.Update(old, st);
            return RedirectToAction("Index");
        }





        //delete student 
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
                //db.Students.Remove(st);
                //db.SaveChanges();
                StRepo.SoftDelete(id.Value);
                return RedirectToAction("Index");

            }
        }

    }
}
