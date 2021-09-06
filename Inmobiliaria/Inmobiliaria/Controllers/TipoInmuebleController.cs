using Inmobiliaria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Controllers
{
    public class TipoInmuebleController : Controller
    {
        RepositorioTipo repositorio;
        public TipoInmuebleController()
        {
            repositorio = new RepositorioTipo();
        }
        // GET: TipoInmuebleController
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerTipoInmuebles();
            return View(lista);
        }

        // GET: TipoInmuebleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoInmuebleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoInmuebleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, TipoInmueble t)
        {
            try
            {
                var res = repositorio.ObtenerPorNombre(t.Tipo);
                if (res != null)
                {
                    return RedirectToAction(nameof(Edit), new { id = res.IdTipo });
                }
                else
                {
                    repositorio.Alta(id, t);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoInmuebleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TipoInmuebleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoInmuebleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TipoInmuebleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
