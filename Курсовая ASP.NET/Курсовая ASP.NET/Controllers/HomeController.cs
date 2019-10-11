using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;

namespace Курсовая_ASP.NET.Controllers
{
    public struct ReportInfo
    {
        public int CountDayBusy;
        public int CountDayFree;
        public int Cost;
        public int CountNum;
        public int Number;
    }
    public class HomeController : Controller
    {
        private HostelEntities db = new HostelEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowCities()
        {
            ViewBag.Cities = db.Cities;
            return View();
        }

        public ActionResult ShowRoomTypes()
        {
            ViewBag.RoomTypes = db.RoomTypes;
            return View();
        }

        public ActionResult ShowСleaning()
        {
            var сleaning = db.Сleaning.Include(с => с.DaysWeek).Include(с => с.Employee).Include(с => с.Floors);
            return View(сleaning.ToList());
        }

        public ActionResult ShowAccommodation()
        {
            var accommodation = db.Accommodation.Include(a => a.Rooms);
            return View(accommodation.ToList());
        }

        public ActionResult ShowResidentsForNumber()
        {
            ViewBag.IdRoom = new SelectList(db.Rooms, "Id", "Number");
            return View();
        }

        public ActionResult ShowResidentsForNumberFun(string idNum)
        {
            int num = int.Parse(idNum);
            var residents = db.Residents.Where(r => r.IdRoom == num).ToList();
            return PartialView(residents);
        }

        public ActionResult ShowResidentsForCity()
        {
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "City");
            return View();
        }

        public ActionResult ShowResidentsForCityFun(string idNum)
        {
            int num = int.Parse(idNum);
            var residents = db.Residents.Where(r => r.IdCity == num).ToList();
            return PartialView(residents);
        }

        public ActionResult ShowReport()
        {
            List<string> quarter = new List<string> {"1 квартал", "2 квартал", "3 квартал", "4 квартал"};
            ViewBag.IdQuarter = new SelectList(quarter);
            return View();
        }

        public ActionResult ShowReportFun(string idQuarter)
        {
            int totalCustomer = 0;
            double totaIncome = 0;
            int num = int.Parse(idQuarter);
            List<ReportInfo> report = new List<ReportInfo>();
            DateTime start = new DateTime();
            DateTime end = new DateTime();
            switch (num)
            {
                case 0:
                    start = new DateTime(DateTime.Today.Year, 1, 1);
                    end = new DateTime(DateTime.Today.Year, 3, 31);
                    break;
                case 1:
                    start = new DateTime(DateTime.Today.Year, 4, 1);
                    end = new DateTime(DateTime.Today.Year, 6, 30);
                    break;
                case 2:
                    start = new DateTime(DateTime.Today.Year, 7, 1);
                    end = new DateTime(DateTime.Today.Year, 9, 30);
                    break;
                case 3:
                    start = new DateTime(DateTime.Today.Year, 10, 1);
                    end = new DateTime(DateTime.Today.Year, 12, 31);
                    break;
            }

            var accommodation = db.Accommodation.Where(ac => ac.SettlementDate < end && (ac.DepartureDate == null || ac.DepartureDate > start)).ToList();
            foreach (var room in db.Rooms)
            {
                ReportInfo rep = new ReportInfo
                {
                    Number = room.Number,
                    CountDayBusy = 0,
                    CountNum = 0
                };
                foreach (var ac in accommodation)
                {
                    if (ac.Rooms.Number != rep.Number) continue;
                    if (ac.SettlementDate >= start && ac.DepartureDate == null)
                    {
                        rep.CountDayBusy += (int)(DateTime.Today - ac.SettlementDate).TotalDays;
                    }
                    else if (ac.SettlementDate < start && ac.DepartureDate == null)
                    {
                        if (start <= DateTime.Today)
                            rep.CountDayBusy += (int)(DateTime.Today - start).TotalDays;
                    }
                    else if (ac.SettlementDate >= start && ac.DepartureDate <= end)
                    {
                        rep.CountDayBusy += (int)(ac.DepartureDate - ac.SettlementDate).Value.TotalDays;
                    }
                    else if (ac.SettlementDate >= start && ac.DepartureDate > end)
                    {
                        rep.CountDayBusy += (int)(end - ac.SettlementDate).TotalDays;
                    }
                    else if (ac.SettlementDate < start && ac.DepartureDate <= end)
                    {
                        rep.CountDayBusy += (int)(ac.DepartureDate - start).Value.TotalDays;
                    }
                    else if (ac.SettlementDate < start && ac.DepartureDate > end)
                    {
                        rep.CountDayBusy += (int)(end - start).TotalDays;
                    }

                    rep.CountNum++;
                }

                if (num == 0 || num == 1)
                {
                    rep.CountDayFree = 91 - rep.CountDayBusy;
                }
                else
                {
                    rep.CountDayFree = 92 - rep.CountDayBusy;
                }

                rep.Cost = (int)(rep.CountDayBusy * room.RoomTypes.Cost.Value);
                totalCustomer += rep.CountNum;
                totaIncome += rep.Cost;
                report.Add(rep);
            }

            ViewBag.TotalCustomer = totalCustomer;
            ViewBag.TotaIncome = totaIncome;
            ViewBag.Report = report;

            return PartialView();
        }
    }
}