using F_Core_CRUD.DB_Folder;
using F_Core_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace F_Core_CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            FInfoContext dbobj = new FInfoContext();
            var res = dbobj.FTb1s.ToList();
            List<F_Model> mobj = new List<F_Model>();
            foreach (var item in res)
            {
                mobj.Add(new F_Model
                {
                    Id = item.Id,
                    Name = item.Name,
                    Age = item.Age,
                    Technology = item.Technology,
                    Email = item.Email
                });

            }
            return View(mobj);
        }
        public IActionResult Edit(int Id)
        {
            F_Model mobj = new F_Model();
            FInfoContext dbobj = new FInfoContext();

            var eobj = dbobj.FTb1s.Where(m => m.Id == Id).First();
            mobj.Id = eobj.Id;
            mobj.Name = eobj.Name;
            mobj.Age = eobj.Age;
            mobj.Email = eobj.Email;
            mobj.Technology = eobj.Technology;

            return View("F_Form",mobj);
        }



        [HttpGet]
        public IActionResult F_Form()
        {
            return View();
        }
       

        [HttpPost]
        public IActionResult F_Form(F_Model mobj)
        {
            FInfoContext dbobj = new FInfoContext();

            FTb1 tobj = new FTb1();

            tobj.Id = mobj.Id;
            tobj.Name = mobj.Name;
            tobj.Age = mobj.Age;
            tobj.Email = mobj.Email;
            tobj.Technology = mobj.Technology;
            if (mobj.Id == 0)
            {
                dbobj.FTb1s.Add(tobj);
                dbobj.SaveChanges();
            }
            else
            {
                dbobj.Entry(tobj).State = EntityState.Modified;
                dbobj.SaveChanges();


            }



            return RedirectToAction("Index");
        }


        public IActionResult Delete(int Id)
        {
            FInfoContext dbobj = new FInfoContext();
            var d_item = dbobj.FTb1s.Where(m => m.Id == Id).First();
            dbobj.FTb1s.Remove(d_item);
            dbobj.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
