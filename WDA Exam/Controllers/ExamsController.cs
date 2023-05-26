using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WDA_Exam.Entities;

namespace WDA_Exam.Controllers
{
    public class ExamsController : Controller
    {
        private readonly AspDotNetExamContext _context;

        public ExamsController(AspDotNetExamContext context)
        {
            _context = context;
        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            var aspDotNetExamContext = _context.Exams.Include(e => e.ClassRoom).Include(e => e.Faculties).Include(e => e.Subjects);
            return View(await aspDotNetExamContext.ToListAsync());
        }

        // GET: Exams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.ClassRoom)
                .Include(e => e.Faculties)
                .Include(e => e.Subjects)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exams/Create
        public IActionResult Create()
        {
            ViewData["ClassRoomId"] = new SelectList(_context.Classrooms, "Id", "Name");
            ViewData["FacultiesId"] = new SelectList(_context.Faculties, "Id", "Name");
            ViewData["SubjectsId"] = new SelectList(_context.Subjects, "Id", "Name");
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubjectsId,StartTime,ExamDate,ExamDuration,ClassRoomId,FacultiesId,Status")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassRoomId"] = new SelectList(_context.Classrooms, "Id", "Name", exam.ClassRoomId);
            ViewData["FacultiesId"] = new SelectList(_context.Faculties, "Id", "Name", exam.FacultiesId);
            ViewData["SubjectsId"] = new SelectList(_context.Subjects, "Id", "Name", exam.SubjectsId);
            return View(exam);
        }

        // GET: Exams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            ViewData["ClassRoomId"] = new SelectList(_context.Classrooms, "Id", "Name", exam.ClassRoomId);
            ViewData["FacultiesId"] = new SelectList(_context.Faculties, "Id", "Name", exam.FacultiesId);
            ViewData["SubjectsId"] = new SelectList(_context.Subjects, "Id", "Name", exam.SubjectsId);
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubjectsId,StartTime,ExamDate,ExamDuration,ClassRoomId,FacultiesId,Status")] Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.Id))
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
            ViewData["ClassRoomId"] = new SelectList(_context.Classrooms, "Id", "Name", exam.ClassRoomId);
            ViewData["FacultiesId"] = new SelectList(_context.Faculties, "Id", "Name", exam.FacultiesId);
            ViewData["SubjectsId"] = new SelectList(_context.Subjects, "Id", "Name", exam.SubjectsId);
            return View(exam);
        }

        // GET: Exams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.ClassRoom)
                .Include(e => e.Faculties)
                .Include(e => e.Subjects)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exams == null)
            {
                return Problem("Entity set 'AspDotNetExamContext.Exams'  is null.");
            }
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
          return (_context.Exams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
