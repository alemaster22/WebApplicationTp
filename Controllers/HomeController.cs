using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTest.Data;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            
             Usuario usuario = new Usuario();
             usuario = _context.Usuario.FirstOrDefault();

             if (usuario == null)
             {
                 Usuario Default = new Usuario();

                 Default.Nombre = "";
                 Default.Id = 0;

                 return View(Default);
             }

             return View(usuario);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if(ModelState.IsValid)
            {
                _context.Usuario.Add(usuario);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View();

        }       

        public IActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Update(usuario);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
            return View();
            
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
