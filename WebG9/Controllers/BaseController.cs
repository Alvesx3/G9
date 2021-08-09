using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebG9.Models;
using WebG9.Repository;

namespace WebG9.Controllers
{
    public class BaseController<T, G> : Controller where T:BaseModel where G: BaseRepository<T>
    {
        G repository;
        public BaseController(G repository)
        {
            this.repository = repository;
        }
            
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(T model)
        {
            repository.Create(model);
            ModelState.Clear();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            T model = repository.Read(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(T model)
        {
            repository.Update(model);
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            return View(repository.Read());
        }
        public ActionResult Delete(int id)
        {
            repository.Delete(id);
            return RedirectToAction("List");
        }
        public ActionResult Details(int id)
        {
            return View(repository.Read(id));
        }
    }
}