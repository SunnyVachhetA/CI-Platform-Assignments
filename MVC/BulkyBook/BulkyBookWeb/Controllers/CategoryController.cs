using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;
public class CategoryController : Controller
{
    private readonly AppDbContext _db;
    public CategoryController(AppDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        //var categoryList = _db.Categories.ToList(); 
        IEnumerable<Category> categoryList = _db.Categories;
        return View( categoryList );
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if(ModelState.IsValid)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();  
            TempData["success"] = "Category Added Successfully";
            return RedirectToAction("Index");
        }
        TempData["failed"] = "Category Creation Failed!";  
        return View(obj);
    }    


    public IActionResult Edit(int? id)
    {
        if(id == null || id == 0)
            return NotFound();
        
        Category? obj = _db.Categories.Find(id);

        return (obj == null) ? NotFound() : View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if(ModelState.IsValid)
        {
            _db.Categories.Update(obj);
            _db.SaveChanges();  
            TempData["success"] = "Category Updated Successfully";
            return RedirectToAction("Index");
        }
        TempData["failed"] = "Category Updatation Failed!";  
        return View();
    }

    public IActionResult Delete(int? id)
    {
        if(id == null || id == 0)
        {
            TempData["failed"] = "Category Not Found!";  
            return NotFound();
        }
        Category? obj = _db.Categories.Find(id);

        return (obj == null) ? NotFound() : View(obj);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST( int? id )
    {
        Category? obj = _db.Categories.Find(id);
        if(obj == null) { 
            TempData["failed"] = "Category Deletion Failed!";  
            return NotFound(); 
        }

        _db.Categories.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Category Deleted Successfully";
        return RedirectToAction("Index");    
    }
}
