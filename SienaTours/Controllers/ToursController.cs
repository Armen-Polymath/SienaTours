using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SienaTours.Data;
using SienaTours.Models;
using SienaTours.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SienaTours.Controllers
{
    public class ToursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tours
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tours.ToListAsync());
        }

        // GET: Tours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // GET: Tours/Create
        public IActionResult Create()
        {
            var vm = new TourFormViewModel
            {
                DifficultyOptions = Enum.GetValues<DifficultyLevel>()
            .Select(d => new SelectListItem
            {
                Value = d.ToString(),
                Text = d.ToString()
            })
            };

            return View(vm);
        }

        // POST: Tours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,DurationMinutes,PricePerPerson,Language,StartLocation,EndLocation,MeetingPointDetails,Difficulty,IsActive,CreatedAt,LastUpdatedAt,Tags")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                tour.CreatedAt = DateTime.UtcNow;
                tour.LastUpdatedAt = DateTime.UtcNow;
                _context.Add(tour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }

        // GET: Tours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tour = await _context.Tours.FindAsync(id);
            if (tour == null) return NotFound();

            var vm = new TourFormViewModel
            {
                Id = tour.Id,
                Name = tour.Name,
                Description = tour.Description,
                DurationMinutes = tour.DurationMinutes,
                PricePerPerson = tour.PricePerPerson,
                Language = tour.Language,
                StartLocation = tour.StartLocation,
                EndLocation = tour.EndLocation,
                MeetingPointDetails = tour.MeetingPointDetails,
                Difficulty = tour.Difficulty,
                IsActive = tour.IsActive,
                Tags = tour.Tags,

                DifficultyOptions = Enum.GetValues<DifficultyLevel>()
                    .Select(d => new SelectListItem { Value = d.ToString(), Text = d.ToString() })
            };

            return View(vm);
        }

        // POST: Tours/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TourFormViewModel vm)
        {
            if (id != vm.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var tour = await _context.Tours.FindAsync(id);
                if (tour == null) return NotFound();

                // Map fields manually
                tour.Name = vm.Name;
                tour.Description = vm.Description;
                tour.DurationMinutes = vm.DurationMinutes;
                tour.PricePerPerson = vm.PricePerPerson;
                tour.Language = vm.Language;
                tour.StartLocation = vm.StartLocation;
                tour.EndLocation = vm.EndLocation;
                tour.MeetingPointDetails = vm.MeetingPointDetails;
                tour.Difficulty = vm.Difficulty;
                tour.IsActive = vm.IsActive;
                tour.Tags = vm.Tags;

                tour.LastUpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Re-populate dropdown if validation fails
            vm.DifficultyOptions = Enum.GetValues<DifficultyLevel>()
                .Select(d => new SelectListItem { Value = d.ToString(), Text = d.ToString() });

            return View(vm);
        }

        // GET: Tours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour != null)
            {
                _context.Tours.Remove(tour);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourExists(int id)
        {
            return _context.Tours.Any(e => e.Id == id);
        }
    }
}
