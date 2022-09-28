using BookShop.DataAccess;
using BookShop.DataAccess.Repository.IRepository;
using BookShop.Utility;
using BookShop.Models;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BookShops.Controllers;
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnviroment;
    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnviroment)
    {
        _unitOfWork = unitOfWork;
        _hostEnviroment = hostEnviroment;
    }


    public IActionResult Index()
    {
        IEnumerable<CoverType> CoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(CoverTypeList);
    }

    //GET
    public IActionResult Upsert(int? id)
    {

        ProductVM productVM = new()
        {
            Product = new(),
            CategoryList = _unitOfWork.Category.GetAll()
            .Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            }),
            CoverTypeList = _unitOfWork.CoverType.GetAll()
            .Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            }),
        };

        if (id == null || id == 0)
        {
            //Create product
            return View(productVM);

        }
        else
        {
            //Update product
        }
        return View(productVM);

    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM obj, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostEnviroment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images/products");
                var extension = Path.GetExtension(file.FileName);

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                obj.Product.ImageUrl = @"/images/products/" + fileName + extension;
            }
            _unitOfWork.Product.Add(obj.Product);
            _unitOfWork.Save();
            TempData["success"] = "Product created successfully";
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
