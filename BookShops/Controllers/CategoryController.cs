using BookShop.DataAccess;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShops.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _db;

    public CategoryController(ICategoryRepository db)
    {
        _db = db;
    }


    public IActionResult Index()
    {
        IEnumerable<Category> categoryList = _db.GetAll();
        return View(categoryList);
    }

    //GET
    public IActionResult Create()
    {
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The display order cannot match the name");
        }
        if (ModelState.IsValid)
        {
            _db.Add(obj);
            _db.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }


    //GET
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var categoryfromDb = _db.GetFirstOrDefault(u=> u.Name=="id");

        if (categoryfromDb == null)
        {
            return NotFound();
        }
        return View(categoryfromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The display order cannot match the name");
        }


        if (ModelState.IsValid)
        {
            _db.Update(obj);
            _db.Save();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        return View(obj);

    }


    //GET
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        // var categoryfromDb = _db.Categories.Find(id);
        var categoryfromDb = _db.GetFirstOrDefault(u=> u.id ==id);

        if (categoryfromDb == null)
        {
            return NotFound();
        }
        return View(categoryfromDb);
    }

    //POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var categoryfromDb = _db.GetFirstOrDefault(u=> u.id ==id);

        if (categoryfromDb == null)
        {
            return NotFound();
        }

        _db.Categories.Remove(categoryfromDb);
        _db.Save();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");

    }


}
