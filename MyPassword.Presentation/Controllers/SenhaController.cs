using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPassword.Entity;
using MyPassword.Presentation.Models;
using MyPassword.Services.Contract;

namespace MyPassword.Presentation.Controllers
{
    public class SenhaController : BaseController
    {
        public readonly ISenhasService _senhasService;
        public readonly IPlataformaService _plataformaService;

        public SenhaController(ISenhasService senhasService, IPlataformaService plataformaService)
        {
            _senhasService = senhasService;
            _plataformaService = plataformaService;
        }

        public ActionResult Index()
        {
            var item = _senhasService.GetAll();
            if (HttpExtensions.IsAjaxRequest(Request))
                return PartialView(item);
            else
                return View(item);
        }

        public ActionResult Edit(int? id)
        {

            var selectListItems = new SelectList(_plataformaService.GetAll(), "PlataformaId", "Nome").ToList();
            selectListItems.Insert(selectListItems.Count(), new SelectListItem { Text = "Outros", Value = "99999" });
            ViewBag.PlataformaId = selectListItems;

            var retorno = new PlataformaSenha();
            retorno.Senha = id.HasValue ? _senhasService.GetById((int)id) : null;        
                       
            return PartialView("_EditPartial", retorno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlataformaSenha plataformaSenha)
        {
            try
            {
                _senhasService.InsertOrUpdate(plataformaSenha);                
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
                _senhasService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return PartialView();
            }
        }

    }
}