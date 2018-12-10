using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspTimeSheet.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using AspTimeSheet.Models;

namespace AspTimeSheet.Controllers
{
    public class StaffPositionsController : Controller
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        private const string controllerMsg = "StaffPositionsController controller log: {0}";

        public StaffPositionsController(ApplicationDbContext context, ILogger<StaffPositionsController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: StaffPositions
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation(controllerMsg, new object[] { "Index get" });

            return View(await _context.StaffPosition.ToListAsync());
        }

        // GET: StaffPositions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffPosition = await _context.StaffPosition
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffPosition == null)
            {
                return NotFound();
            }

            return View(staffPosition);
        }

        // GET: StaffPositions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StaffPositions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] StaffPosition staffPosition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffPosition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staffPosition);
        }

        // GET: StaffPositions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffPosition = await _context.StaffPosition.FindAsync(id);
            if (staffPosition == null)
            {
                return NotFound();
            }
            return View(staffPosition);
        }

        // POST: StaffPositions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] StaffPosition staffPosition)
        {
            if (id != staffPosition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffPosition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffPositionExists(staffPosition.Id))
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
            return View(staffPosition);
        }

        // GET: StaffPositions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffPosition = await _context.StaffPosition
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffPosition == null)
            {
                return NotFound();
            }

            return View(staffPosition);
        }

        // POST: StaffPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffPosition = await _context.StaffPosition.FindAsync(id);
            _context.StaffPosition.Remove(staffPosition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffPositionExists(int id)
        {
            return _context.StaffPosition.Any(e => e.Id == id);
        }

        // GET: StaffPositions/Delete/5
        public async Task<IActionResult> Select()
        {
            _logger.LogInformation(controllerMsg, new object[] { "Select get" });

            return View(await _context.StaffPosition.Select(x=>_mapper.Map<StaffPositionModelView>(x)).ToListAsync());
        }

    }
}
