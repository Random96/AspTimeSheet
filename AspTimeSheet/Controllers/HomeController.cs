using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspTimeSheet.Models;
using Microsoft.Extensions.Logging;
using AspTimeSheet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspTimeSheet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        private const string homeMsg = "Home controller log: {0}";

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// показ представления по умолчанию. Список отсутствия
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(HookyFilterModelView filter)
        {
            _logger.LogInformation(homeMsg, new object[] { "Index get" });

            ViewBag.Filter = filter;

            IQueryable<Hooky> hookData = _context.Hooky.Include(x=>x.Person).Include(x=>x.Position);            
            hookData = filter.Apply(hookData);
            var ret = await hookData.Select(x => _mapper.Map<HookyModelView>(x)).ToListAsync();
            return View(ret);
        }

        public IActionResult Filter()
        {
            _logger.LogInformation(homeMsg, new object[] { "Filter get" });

            return View();
        }

        [HttpPost]
        public IActionResult Filter(HookyFilterModelView filter)
        {
            _logger.LogInformation(homeMsg, new object[] { "Index parameters get" });

            return RedirectToAction("Index", filter);
        }

        public async Task<IActionResult> Create()
        {
            var positions = await _context.StaffPosition.OrderBy(x=>x.Name).ToListAsync();
            ViewBag.Positions = new SelectList(positions, "Id", "Name");

            var persons = await _context.Person.Select(x=> new { x.Id, Name = $"{x.LastName} {x.Name} {x.MiddleName}" }).OrderBy(x => x.Name).ToListAsync();
            ViewBag.Persons = new SelectList(persons, "Id", "Name");
            return View();
        }

        /// <summary>
        /// Сохранение нового значения
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(HookyModelView model)
        {
            _logger.LogInformation("method post of create");

            if (ModelState.IsValid)
            {
                _context.Add(_mapper.Map<Hooky>(model));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult About()
        {
            _logger.LogInformation("About get");

            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            _logger.LogInformation("Contact get");

            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Privacy get");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation("Error");

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
