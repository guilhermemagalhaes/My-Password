using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPassword.Entity;
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
            var item = _plataformaService.GetAll();
            if (HttpExtensions.IsAjaxRequest(Request))
                return PartialView(item);
            else
                return View(item);
        }


        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return PartialView("_EditPartial", new Plataforma());
            else
                return PartialView("_EditPartial", _plataformaService.GetById((int)id));
        }

        [HttpPost]
        public ActionResult Edit(Plataforma plataforma)
        {
            try
            {
                _plataformaService.InsertOrUpdate(plataforma);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return PartialView();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _plataformaService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return PartialView();
            }
        }
    }
}