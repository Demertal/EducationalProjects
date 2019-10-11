using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Курсовая_ASP.NET;

namespace Курсовая_ASP.NET.Controllers
{
    public class ResidentsController : Controller
    {
        private HostelEntities db = new HostelEntities();

        // GET: Residents
        public ActionResult Index(bool IsShowEvicted = false)
        {
            var residents = db.Residents.Include(r => r.Cities).Include(r => r.Rooms);
            ViewBag.Residents = residents.ToList();
            ViewBag.IsShowEvicted = IsShowEvicted;
            return View(ViewBag);
        }
        // GET: Residents/Create
        public ActionResult Create()
        {
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "City");
            ViewBag.IdRoom = new SelectList(db.Rooms, "Id", "Number");
            return View();
        }

        // POST: Residents/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Passport,Name,Patronymic,Surname,IdCity,SettlementDate,IdRoom,IsEvicted")] Residents residents)
        {
            if (string.IsNullOrEmpty(residents.Passport) || residents.Passport.Length != 10)
            {
                ModelState.AddModelError("Patronymic", "Некорректный паспорт");
            }
            if (string.IsNullOrEmpty(residents.Name))
            {
                ModelState.AddModelError("Name", "Некорректное имя");
            }
            if (string.IsNullOrEmpty(residents.Patronymic))
            {
                ModelState.AddModelError("Patronymic", "Некорректное отчество");
            }
            if (string.IsNullOrEmpty(residents.Surname))
            {
                ModelState.AddModelError("Surname", "Некорректное фамилия");
            }

            Rooms room = db.Rooms.Find(residents.IdRoom);
            if (room?.FreePlace == 0)
            {
                ModelState.AddModelError("IdRoom", "В номере нет свободных мест");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Residents.Add(residents);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return View("Error");
                }

                return RedirectToAction("Index");
            }

            ViewBag.IdCity = new SelectList(db.Cities, "Id", "City", residents.IdCity);
            ViewBag.IdRoom = new SelectList(db.Rooms, "Id", "Number", residents.IdRoom);
            return View(residents);
        }

        // GET: Residents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residents residents = db.Residents.Find(id);
            if (residents == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "City", residents.IdCity);
            ViewBag.IdRoom = new SelectList(db.Rooms, "Id", "Number", residents.IdRoom);
            return View(residents);
        }

        // POST: Residents/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Passport,Name,Patronymic,Surname,IdCity,SettlementDate,IdRoom,IsEvicted")] Residents residents)
        {
            if (string.IsNullOrEmpty(residents.Passport) || residents.Passport.Length != 10)
            {
                ModelState.AddModelError("Patronymic", "Некорректный паспорт");
            }
            if (string.IsNullOrEmpty(residents.Name))
            {
                ModelState.AddModelError("Name", "Некорректное имя");
            }
            if (string.IsNullOrEmpty(residents.Patronymic))
            {
                ModelState.AddModelError("Patronymic", "Некорректное отчество");
            }
            if (string.IsNullOrEmpty(residents.Surname))
            {
                ModelState.AddModelError("Surname", "Некорректное фамилия");
            }

            Rooms room = db.Rooms.Find(residents.IdRoom);
            if (room?.FreePlace == 0)
            {
                ModelState.AddModelError("IdRoom", "В номере нет свободных мест");
            }
            if (ModelState.IsValid)
            {
                db.Entry(residents).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "City", residents.IdCity);
            ViewBag.IdRoom = new SelectList(db.Rooms, "Id", "Number", residents.IdRoom);
            return View(residents);
        }

        // GET: Residents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residents residents = db.Residents.Find(id);
            if (residents == null)
            {
                return HttpNotFound();
            }
            return View(residents);
        }

        // POST: Residents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "DepartureDate")]int id, DateTime? departureDate)
        {
            Residents residents = db.Residents.Find(id);
            if (departureDate == null)
            {
                ModelState.AddModelError("DepartureDate", "Нужно указать дату выселения");
            }
            else if (departureDate < residents.SettlementDate)
            {
                ModelState.AddModelError("DepartureDate", "Дата выселения не может быть раньше даты заселения");
            }
            if (!ModelState.IsValid)
            {
                return View(residents);
            }
            residents.IsEvicted = true;
            Accommodation accommodation = db.Accommodation.FirstOrDefault(ac =>
                ac.IdRoom == residents.IdRoom && ac.SettlementDate == residents.SettlementDate &&
                ac.DepartureDate == null);
            accommodation.DepartureDate = departureDate;
            Rooms rooms = db.Rooms.Find(residents.IdRoom);
            rooms.FreePlace++;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
