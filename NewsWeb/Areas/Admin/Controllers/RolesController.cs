using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsWeb.Models;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace NewsWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class RolesController : Controller
  {
    private readonly NewsWebContext _context;

    public RolesController(NewsWebContext context)
    {
      _context = context;
    }

    // GET: Admin/Roles
    public IActionResult Index(int? page)
    {
      var pageNumber = page ?? 1;
      ViewBag.roles = _context.Roles.ToList().ToPagedList(pageNumber, 5);
      return View();
    }

    // CheckRole


    // GET: Admin/Roles/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var role = await _context.Roles
          .FirstOrDefaultAsync(m => m.RoleId == id);
      if (role == null)
      {
        return NotFound();
      }

      return View(role);
    }

    // GET: Admin/Roles/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Admin/Roles/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("RoleId,RoleName,RoleDescription")] Role role)
    {
      if (ModelState.IsValid)
      {
        _context.Add(role);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(role);
    }

    // GET: Admin/Roles/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var role = await _context.Roles.FindAsync(id);
      if (role == null)
      {
        return NotFound();
      }
      return View(role);
    }

    // POST: Admin/Roles/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("RoleId,RoleName,RoleDescription")] Role role)
    {
      if (id != role.RoleId)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(role);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!RoleExists(role.RoleId))
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
      return View(role);
    }

    // GET: Admin/Roles/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var role = await _context.Roles
          .FirstOrDefaultAsync(m => m.RoleId == id);
      if (role == null)
      {
        return NotFound();
      }

      return View(role);
    }

    // POST: Admin/Roles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var role = await _context.Roles.FindAsync(id);
      _context.Roles.Remove(role);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool RoleExists(int id)
    {
      return _context.Roles.Any(e => e.RoleId == id);
    }
  }
}
