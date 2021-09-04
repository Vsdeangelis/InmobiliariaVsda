using Inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Controllers
{
    public class InquilinoController : Controller
    {
        RepositorioInquilino repositorio;
        public InquilinoController()
        {
            repositorio = new RepositorioInquilino();
        }
        // GET: InquilinoController
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerInquilinos();
            return View(lista);
        }

        // GET: InquilinoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InquilinoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InquilinoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, Persona p)
        {
            try
            {
                var res = repositorio.ObtenerPorDni(p.Dni);
                if (res != null)
                {
                    return RedirectToAction(nameof(Edit), new { id = res.IdPersona });
                }
                else
                {
                    repositorio.Alta(id, p);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: InquilinoController/Edit/5
        public ActionResult Edit(int id)
        {
            var resp = repositorio.ObtenerPorId(id);
            return View(resp);
        }

        // POST: InquilinoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Persona res)
        {
            try
            {
                repositorio.Alta(id, res);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InquilinoController/Delete/5
        public ActionResult Delete(int id)
        {
            var res = repositorio.ObtenerPorId(id);
            return View(res);
        }

        // POST: InquilinoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Persona p)
        {
            try
            {
                repositorio.Baja(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
