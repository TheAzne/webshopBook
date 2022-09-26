using BookShop.DataAccess;
using BookShop.DataAccess.Repository.IRepository;
using BookShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;




namespace BookShops.Controllers;

public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public IActionResult Index()
    {
        IEnumerable<CoverType> CoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(CoverTypeList);
    }

    //GET
    public IActionResult Upsert(int? id)
    {
        Product product = new();
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
            u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
        IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
   u => new SelectListItem
   {
       Text = u.Name,
       Value = u.Id.ToString()
   });



        if (id == null || id == 0)
        {
            //Create product
            ViewBag.CategoryList = CategoryList;
            return View(product);
        }
        else
        {
            //Update product
        }


        return View(product);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(CoverType obj)
    {

        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType updated successfully";
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
        var CoverTypefromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

        if (CoverTypefromDb == null)
        {
            return NotFound();
        }
        return View(CoverTypefromDb);
    }

    //POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var CoverTypefromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

        if (CoverTypefromDb == null)
        {
            return NotFound();
        }

        _unitOfWork.CoverType.Remove(CoverTypefromDb);
        _unitOfWork.Save();
        TempData["success"] = "CoverType deleted successfully";
        return RedirectToAction("Index");

    }


}
