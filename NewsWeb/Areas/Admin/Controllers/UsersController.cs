﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsWeb.Helpers;
using NewsWeb.Models;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace NewsWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class UsersController : Controller
  {
    private readonly NewsWebContext _context;

    public UsersController(NewsWebContext context)
    {
      _context = context;
    }

    // GET: Admin/Users
    public IActionResult Index(int? page)
    {
      var pageNumber = page ?? 1;
      ViewBag.Users = _context.Users.ToList().ToPagedList(pageNumber, Utilities.PAGE_SIZE);
      return View();
    }

    // GET: Admin/Users/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var user = await _context.Users
          .Include(u => u.Role)
          .FirstOrDefaultAsync(m => m.UserId == id);
      if (user == null)
      {
        return NotFound();
      }

      return View(user);
    }

    // GET: Admin/Users/Create
    public IActionResult Create()
    {
      ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId");
      return View();
    }

    // POST: Admin/Users/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("UserId,UserName,Email,PhoneNumber,Password,RoleId,Active,LastLogin")] User user)
    {
      if (ModelState.IsValid)
      {
        _context.Add(user);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", user.RoleId);
      return View(user);
    }

    // GET: Admin/Users/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound();
      }
      ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", user.RoleId);
      return View(user);
    }

    // POST: Admin/Users/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,Email,PhoneNumber,Password,RoleId,Active,LastLogin")] User user)
    {
      if (id != user.UserId)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(user);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!UserExists(user.UserId))
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
      ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", user.RoleId);
      return View(user);
    }

    // GET: Admin/Users/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var user = await _context.Users
          .Include(u => u.Role)
          .FirstOrDefaultAsync(m => m.UserId == id);
      if (user == null)
      {
        return NotFound();
      }

      return View(user);
    }

    // POST: Admin/Users/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var user = await _context.Users.FindAsync(id);
      _context.Users.Remove(user);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool UserExists(int id)
    {
      return _context.Users.Any(e => e.UserId == id);
    }
  }
}
