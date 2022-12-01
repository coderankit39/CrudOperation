using CrudOperation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Index
        public async Task<IActionResult> Index(int pageNumber=1)
        {
            return View(await PaginatedList<Category>.CreateAsync(_context.Categories,pageNumber,5));
        }

        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                var cat = new Category()
                {
                    CategoryName = model.CategoryName
                };
                _context.Categories.Add(cat);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["errror"] = "Empty field Can't Submit";
                return View(model);
            }
        }

        //Delete
        public IActionResult Delete(int id)
        {
            var cat = _context.Categories.SingleOrDefault(c => c.CategoryId == id);
            _context.Categories.Remove(cat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Edit
        public IActionResult Edit(int id)
        {
            var cat = _context.Categories.SingleOrDefault(c => c.CategoryId == id);
            return View(cat);

        }

        [HttpPost]
        public IActionResult Edit(Category model)
        {
            _context.Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
