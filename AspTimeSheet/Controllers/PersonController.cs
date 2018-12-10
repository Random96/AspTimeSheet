using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspTimeSheet.Data;
using Microsoft.Extensions.Logging;
using AutoMapper;
using AspTimeSheet.Models;

namespace AspTimeSheet.Controllers
{
    /// <summary>
    /// Контроллер списка работников
    /// </summary>
    public class PersonController : Controller
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        private const string controllerMsg = "PersonController controller log: {0}";

        public PersonController(ApplicationDbContext context, ILogger<StaffPositionsController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation(controllerMsg, new object[] { "Index get" });

            return View(await _context.Person.Select(x=>_mapper.Map<PersonModelView>(x)).ToListAsync());
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            _logger.LogInformation(controllerMsg, new object[] { $"Detail get with id = {id}" });

            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<PersonModelView>(person));
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            _logger.LogInformation(controllerMsg, new object[] { "Create get" });

            return View();
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MiddleName,LastName,PersonnelNumber")] PersonModelView person)
        {
            _logger.LogInformation(controllerMsg, new object[] { $"create post" });

            if (ModelState.IsValid)
            {
                _context.Add(_mapper.Map<Person>(person));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogInformation(controllerMsg, new object[] { $"Edit get with id = {id}" });

            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<PersonModelView>(person));
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MiddleName,LastName,PersonnelNumber")] PersonModelView person)
        {
            _logger.LogInformation(controllerMsg, new object[] { $"Edit post with id = {id}" });

            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<Person>(person));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            _logger.LogInformation(controllerMsg, new object[] { $"delete get with id = {id}" });

            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<PersonModelView>(person));
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation(controllerMsg, new object[] { $"Detail confirm post with id = {id}" });

            var person = await _context.Person.FindAsync(id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
