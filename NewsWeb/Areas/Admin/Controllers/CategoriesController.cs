using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsWeb.Helpers;
using NewsWeb.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace NewsWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class CategoriesController : Controller
  {
    private readonly NewsWebContext _context;

    public CategoriesController(NewsWebContext context)
    {
      _context = context;
    }

    // GET: Admin/Categories
    public IActionResult Index(int? page)
    {
      var pageNumber = page ?? 1;
      ViewBag.Categories = _context.Categories.ToList().ToPagedList(pageNumber, Utilities.PAGE_SIZE);
      return View();
    }

    // GET: Admin/Categories/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var category = await _context.Categories
          .FirstOrDefaultAsync(m => m.CategoryId == id);
      if (category == null)
      {
        return NotFound();
      }

      return View(category);
    }

    // GET: Admin/Categories/Create
    public IActionResult Create()
    {
      ViewData["DanhMucGoc"] = new SelectList(_context.Categories.Where(x => x.Levels == 1), "CategoryId", "CategoryName");
      return View();
    }

    // POST: Admin/Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Title,Alias,MetaDesc,MetaKey,Thumb,Published,Ordering,Parents,Levels,Icon,Cover,Description")] Category category, Microsoft.AspNetCore.Http.IFormFile fThumb, Microsoft.AspNetCore.Http.IFormFile fCover, Microsoft.AspNetCore.Http.IFormFile fIcon)
    {
      if (ModelState.IsValid)
      {
        category.Alias = Utilities.FriendlyUrl(category.CategoryName);
        if (category.Parents == null)
        {
          category.Levels = 1;
        }
        else
        {
          category.Levels = category.Parents == 0 ? 1 : 2;
        }

        if (fThumb != null)
        {
          string extension = Path.GetExtension(fThumb.FileName);
          string newName = Utilities.FriendlyUrl(category.CategoryName) + "_preview" + extension;
          category.Thumb = await Utilities.UploadFile(fThumb, @"categories\", newName.ToLower());
        }
        if (fCover != null)
        {
          string extension = Path.GetExtension(fCover.FileName);
          string newName = "Cover_" + Utilities.FriendlyUrl(category.CategoryName) + extension;
          category.Cover = await Utilities.UploadFile(fCover, @"categories\", newName.ToLower());
        }
        if (fIcon != null)
        {
          string extension = Path.GetExtension(fIcon.FileName);
          string newName = "Icon_" + Utilities.FriendlyUrl(category.CategoryName) + extension;
          category.Icon = await Utilities.UploadFile(fIcon, @"categories\", newName.ToLower());
        }
        _context.Add(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(category);
    }

    // GET: Admin/Categories/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var category = await _context.Categories.FindAsync(id);
      if (category == null)
      {
        return NotFound();
      }
      ViewData["DanhMucGoc"] = new SelectList(_context.Categories.Where(x => x.Levels == 1), "CategoryId", "CategoryName");
      return View(category);
    }

    // POST: Admin/Categories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Title,Alias,MetaDesc,MetaKey,Thumb,Published,Ordering,Parents,Levels,Icon,Cover,Description")] Category category, Microsoft.AspNetCore.Http.IFormFile fThumb, Microsoft.AspNetCore.Http.IFormFile fCover, Microsoft.AspNetCore.Http.IFormFile fIcon)
    {
      if (id != category.CategoryId)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          category.Alias = Utilities.FriendlyUrl(category.CategoryName);
          if (category.Parents == null)
          {
            category.Levels = 1;
          }
          else
          {
            category.Levels = category.Parents == 0 ? 1 : 2;
          }

          if (fThumb != null)
          {
            string extension = Path.GetExtension(fThumb.FileName);
            string newName = Utilities.FriendlyUrl(category.CategoryName) + "_preview" + extension;
            category.Thumb = await Utilities.UploadFile(fThumb, @"categories\", newName.ToLower());
          }
          if (fCover != null)
          {
            string extension = Path.GetExtension(fCover.FileName);
            string newName = "Cover_" + Utilities.FriendlyUrl(category.CategoryName) + extension;
            category.Cover = await Utilities.UploadFile(fCover, @"categories\", newName.ToLower());
          }
          if (fIcon != null)
          {
            string extension = Path.GetExtension(fIcon.FileName);
            string newName = "Icon_" + Utilities.FriendlyUrl(category.CategoryName) + extension;
            category.Icon = await Utilities.UploadFile(fIcon, @"categories\", newName.ToLower());
          }

          _context.Update(category);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!CategoryExists(category.CategoryId))
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
      return View(category);
    }

    // GET: Admin/Categories/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var category = await _context.Categories
          .FirstOrDefaultAsync(m => m.CategoryId == id);
      if (category == null)
      {
        return NotFound();
      }

      return View(category);
    }

    // POST: Admin/Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var category = await _context.Categories.FindAsync(id);
      _context.Categories.Remove(category);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists(int id)
    {
      return _context.Categories.Any(e => e.CategoryId == id);
    }
  }
}
