using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Курсовая_ASP.NET.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly HostelEntities _db = new HostelEntities();

        // GET: Employees
        public ActionResult Index()
        {
            return View(_db.Employee.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _db.Employee.Find(id);
      
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Patronymic,Surname")] Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Name))
            {
                ModelState.AddModelError("Name", "Некорректное имя");
            }
            if (string.IsNullOrEmpty(employee.Patronymic))
            {
                ModelState.AddModelError("Patronymic", "Некорректное отчество");
            }
            if (string.IsNullOrEmpty(employee.Surname))
            {
                ModelState.AddModelError("Surname", "Некорректное фамилия");
            }
            if (ModelState.IsValid)
            {
                _db.Employee.Add(employee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Patronymic,Surname")] Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Name))
            {
                ModelState.AddModelError("Name", "Некорректное имя");
            }
            if (string.IsNullOrEmpty(employee.Patronymic))
            {
                ModelState.AddModelError("Patronymic", "Некорректное отчество");
            }
            if (string.IsNullOrEmpty(employee.Surname))
            {
                ModelState.AddModelError("Surname", "Некорректное фамилия");
            }
            if (ModelState.IsValid)
            {
                _db.Entry(employee).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = _db.Employee.Find(id);
            _db.Employee.Remove(employee ?? throw new InvalidOperationException());
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AddСleaning(int id)
        {
            Employee employee = _db.Employee.Find(id);
            Сleaning cleaning = new Сleaning
            {
                Employee = employee,
                IdEmployee = employee.Id
            };
            ViewBag.IdDay = new SelectList(_db.DaysWeek, "Id", "Day");
            ViewBag.IdFloor = new SelectList(_db.Floors, "Id", "Floor");
            return View(cleaning);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddСleaning([Bind(Include = "Id,IdEmployee,IdFloor,IdDay")] Сleaning сleaning)
        {
            if (ModelState.IsValid)
            {
                _db.Сleaning.Add(сleaning);
                _db.SaveChanges();
                return RedirectToAction("Details/"+сleaning.IdEmployee);
            }

            ViewBag.IdDay = new SelectList(_db.DaysWeek, "Id", "Day", сleaning.IdDay);
            ViewBag.IdEmployee = new SelectList(_db.Employee, "Id", "Name", сleaning.IdEmployee);
            ViewBag.IdFloor = new SelectList(_db.Floors, "Id", "Id", сleaning.IdFloor);
            return View(сleaning);
        }

        public ActionResult DeleteСleaning(int id)
        {
            Сleaning cl = _db.Сleaning.Find(id);
            _db.Сleaning.Remove(cl ?? throw new InvalidOperationException());
            _db.SaveChanges();
            return RedirectToAction("Details/" + cl.IdEmployee);
        }
    }
}
