using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaCRUD.Models;

namespace PruebaCRUD.Controllers
{
    public class UsersController : Controller
    {
        private readonly PruebaContext _context;

        public UsersController(PruebaContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.FromSqlRaw("EXECUTE userGetAll").ToListAsync();

            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = _context.Users.FromSqlRaw("EXECUTE userGetAllById @id", new SqlParameter("@id", id)).AsEnumerable().FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,LastName,DocumentType,DocumentNumber,BirthDate,Telephone,Address,City")] User user)
        {
            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC userCreate @name, @lastName, @documentType, @documentNumber, @birthDate, @telephone, @address, @city",
                    new SqlParameter("@name", user.Name),
                    new SqlParameter("@lastName", user.LastName),
                    new SqlParameter("@documentType", user.DocumentType),
                    new SqlParameter("@documentNumber", user.DocumentNumber),
                    new SqlParameter("@birthDate", user.BirthDate),
                    new SqlParameter("@telephone", user.Telephone),
                    new SqlParameter("@address", user.Address),
                    new SqlParameter("@city", user.City)
                );

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = _context.Users.FromSqlRaw("EXECUTE userGetAllById @id", new SqlParameter("@id", id)).AsEnumerable().FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LastName,DocumentType,DocumentNumber,BirthDate,Telephone,Address,City")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC userUpdate @id, @name, @lastName, @documentType, @documentNumber, @birthDate, @telephone, @address, @city",
                    new SqlParameter("@id", user.Id),
                    new SqlParameter("@name", user.Name),
                    new SqlParameter("@lastName", user.LastName),
                    new SqlParameter("@documentType", user.DocumentType),
                    new SqlParameter("@documentNumber", user.DocumentNumber),
                    new SqlParameter("@birthDate", user.BirthDate),
                    new SqlParameter("@telephone", user.Telephone),
                    new SqlParameter("@address", user.Address),
                    new SqlParameter("@city", user.City)
                );

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = _context.Users.FromSqlRaw("EXECUTE userGetAllById @id", new SqlParameter("@id", id)).AsEnumerable().FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'PruebaContext.Users'  is null.");
            }

            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC userDelete @id",new SqlParameter("@id", id));

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
