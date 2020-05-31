using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPassword.Services.Contract;

namespace MyPassword.Presentation.Controllers
{
    public class PlataformaController : BaseController
    {
        public readonly IPlataformaService _plataformaService;
        public PlataformaController(IPlataformaService plataformaService)
        {
            _plataformaService = plataformaService;
        }


        // GET: Plataforma
        public ActionResult Index()
        {
            var item = _plataformaService.GetPlataformas();
            if (HttpExtensions.IsAjaxRequest(Request))
                return PartialView(item);
            else
                return View(item);
        }

        // GET: Plataforma/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Plataforma/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Plataforma/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Plataforma/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Plataforma/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Plataforma/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Plataforma/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}