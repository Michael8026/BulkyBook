﻿using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
                
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _context.Categories;
            
            return View(objCategoryList);
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
            //to create a custom validation (It displays if theres a validation summary)
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the name"); //if we customised the "CustomError" to the name of a property it displays it under the property field
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Category Created Successfully";
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
            var dbCategory = _context.Categories.Find(id);
            //var dbCategoryFirst = _context.Categories.FirstOrDefault(u => u.Id == id);
            //var dbcategorySingle = _context.Categories.SingleOrDefault(u => u.Id == id);

            if (dbCategory == null)
            {
                return NotFound();
            }

            return View(dbCategory);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            //to create a custom validation (It displays if theres a validation summary)
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the name"); //if we customised the "CustomError" to the name of a property it displays it under the property field
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
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
            var dbCategory = _context.Categories.Find(id);
            //var dbCategoryFirst = _context.Categories.FirstOrDefault(u => u.Id == id);
            //var dbcategorySingle = _context.Categories.SingleOrDefault(u => u.Id == id);

            if (dbCategory == null)
            {
                return NotFound();
            }

            return View(dbCategory);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _context.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");










        }
    }
}
