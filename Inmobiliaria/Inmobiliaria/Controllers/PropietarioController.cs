using Inmobiliaria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Controllers
{
    public class PropietarioController : Controller
    {
        RepositorioPropietario repositorio;
        public PropietarioController()
        {
            repositorio = new RepositorioPropietario();
        }
        // GET: PersonasController
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerPropietarios();
            return View(lista);
        }

        // GET: PersonasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, Persona p)
        {
            try
            {
                var res = repositorio.ObtenerPorDni(p.Dni);
                if (res != null)
                {
                    return RedirectToAction(nameof(Edit),new { id= res.IdPersona });
                }
                else
                {
                    repositorio.Alta(id, p);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: PersonasController/Edit/5
        public ActionResult Edit(int id)
        {
            var resp = repositorio.ObtenerPorId(id);
            return View(resp);
        }

        // POST: PersonasController/Edit/5
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

        // GET: PersonasController/Delete/5
        public ActionResult Delete(int id)
        {
            var res=repositorio.ObtenerPorId(id);
            return View(res);
        }

        // POST: PersonasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Persona res)
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
