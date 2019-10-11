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
    public class RoomsController : Controller
    {
        private readonly HostelEntities _db = new HostelEntities();

        // GET: Rooms
        public ActionResult Index()
        {
            var rooms = _db.Rooms.Include(r => r.Floors).Include(r => r.RoomTypes);
            return View(rooms.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
