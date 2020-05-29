using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using assignment_03.Models;

namespace assignment_03.Controllers
{
    
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        prayersDataContext p = new prayersDataContext();

        public ActionResult Index()
        {
            List<string> areas = p.prayers
                        .Select(column => column.masjid_area).ToList();
            List<string> areas1 = new HashSet<string>(areas).ToList();
            ViewData["area_list"] = areas1;
            return View();
        }
        public ActionResult add()
        {
            string name = Request["name"];
            string area = Request["area"];
            string fajar_time = Request["fhours"] + ":" + Request["fmin"] + ":" + Request["fam"];
            string zohar_time = Request["zhours"] + ":" + Request["zmin"] + ":" + Request["zam"];
            string asar_time = Request["ahours"] + ":" + Request["amin"] + ":" + Request["aam"];
            string maghrib_time = Request["mhours"] + ":" + Request["mmin"] + ":" + Request["mam"];
            string esha_time = Request["ehours"] + ":" + Request["emin"] + ":" + Request["eam"];
            if (name == "" || area == "")
                return View("error");

            prayer pr = new prayer();
            pr.majid_name = name;
            pr.masjid_area = area;
            pr.fajar_time = fajar_time;
            pr.zohar_time = zohar_time;
            pr.asar_time = asar_time;
            pr.maghrib_time = maghrib_time;
            pr.esha_time = esha_time;

            p.prayers.InsertOnSubmit(pr);
            p.SubmitChanges();
            return RedirectToAction("addSuc");
        }
        public ActionResult addSuc()
        {
            return View();
        }
        public ActionResult find()
        {
            string area=Request["areas"];
            List<prayer> prayer1 = p.prayers.Where(m => m.masjid_area == area).ToList();
            return View(prayer1);
        }
        public ActionResult edit()
        {
            string name = Request["name"];
            prayer prayer1 = p.prayers.First(m => m.majid_name == name);
            return View(prayer1);
        }
        public ActionResult edit1()
        {
            string name = Request["name"];
            string area = Request["area"];
            string fajar_time = Request["fhours"] + ":" + Request["fmin"] + ":" + Request["fam"];
            string zohar_time = Request["zhours"] + ":" + Request["zmin"] + ":" + Request["zam"];
            string asar_time = Request["ahours"] + ":" + Request["amin"] + ":" + Request["aam"];
            string maghrib_time = Request["mhours"] + ":" + Request["mmin"] + ":" + Request["mam"];
            string esha_time = Request["ehours"] + ":" + Request["emin"] + ":" + Request["eam"];
            if (name == "" || area == "")
                return View("error");
            string name1 = Request["prename"];
            prayer prayer1 = p.prayers.First(m => m.majid_name == name1);
            p.prayers.DeleteOnSubmit(prayer1);
            p.SubmitChanges();
            prayer pr = new prayer();
            pr.majid_name = name;
            pr.masjid_area = area;
            pr.fajar_time = fajar_time;
            pr.zohar_time = zohar_time;
            pr.asar_time = asar_time;
            pr.maghrib_time = maghrib_time;
            pr.esha_time = esha_time;

            p.prayers.InsertOnSubmit(pr);
            p.SubmitChanges();
            return RedirectToAction("index");
        }
        public ActionResult delete()
        {
            string name = Request["name"];
            prayer prayer1 = p.prayers.First(m => m.majid_name == name);
            p.prayers.DeleteOnSubmit(prayer1);
            p.SubmitChanges();
            return RedirectToAction("index");
        }
    }
}
