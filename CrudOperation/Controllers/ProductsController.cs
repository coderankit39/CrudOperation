using CrudOperation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Index 
        public async Task<IActionResult> IndexAsync(int pageNumber = 1)
        {
           
            return View(await PaginatedList<Product>.CreateAsync(_context.Products.Include(p => p.Category), pageNumber, 10));
        }

        //Create
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,CategoryId,")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = GetCategories();
            return View(product);
        }

        //Edit
        public IActionResult Edit(int id)
        {
            Product product = _context.Products.Where(p => p.ProductId == id ).FirstOrDefault();
            ViewBag.Categories = GetCategories();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        //Delete
        public IActionResult Delete(int? id)
        {
            var pro = _context.Products.Include(p => p.Category).SingleOrDefault(m => m.ProductId == id);
            _context.Products.Remove(pro);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        private List<SelectListItem> GetCategories()
        {
            var lstCategories = new List<SelectListItem>();

            List<Category> categories = _context.Categories.ToList(); 
            
            lstCategories = categories.Select(ct => new SelectListItem()
            {
                Value = ct.CategoryId.ToString(),
                Text = ct.CategoryName   
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "---Slect Category---"
            };
            lstCategories.Insert(0, defItem);
            return lstCategories;
        }
    }
}
