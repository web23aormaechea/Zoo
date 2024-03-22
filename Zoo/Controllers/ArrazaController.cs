using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Zoo.Data;
using Zoo.Models;
using Zoo.Models.ViewModels;
using Zoo.Repository.IRepository;

namespace Zoo.Controllers
{
    public class ArrazaController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ILekuaRepository _LekuaRepo;
        public ArrazaController(IUnitOfWork unitOfWork, ILekuaRepository db2, IWebHostEnvironment webHostEnvironment)
        {
            _UnitOfWork = unitOfWork;
            _LekuaRepo = db2;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public IActionResult Index()
        {
            List<Arraza> objArrazaList = _UnitOfWork.Arraza.GetAll().ToList();
            List<Lekua> objLekuaList = _LekuaRepo.GetAll().ToList();
            ViewBag.objLekuaList = objLekuaList;
            return View(objArrazaList);
        }
        
        public IActionResult List()
        {
            List<Arraza> objArrazaList = _UnitOfWork.Arraza.GetAll().ToList();
            List<Lekua> objLekuaList = _LekuaRepo.GetAll().ToList();
            ViewBag.objLekuaList = objLekuaList;
            return View(objArrazaList);
        }
        public IActionResult Upsert(int? ID)
        {
            ArrazaVM arrazaVM = new() 
            {
                LekuakList = _LekuaRepo.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Izena,
                    Value = u.ID.ToString()
                }),
                Arraza = new Arraza()
            };
            if (ID == null) {
                //create
                return View(arrazaVM);
            }
            else
            {
                //update
                arrazaVM.Arraza = _UnitOfWork.Arraza.Get(u=>u.ID==ID);
                return View(arrazaVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ArrazaVM arrazaVM, IFormFile? file)
        {
             if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string arrazaPath = Path.Combine(wwwRootPath, @"images\arraza");
                    if (!string.IsNullOrEmpty(arrazaVM.Arraza.ImageUrl))
                    {
                        //Delete the old image in the update
                        var oldImagePath = Path.Combine(wwwRootPath, arrazaVM.Arraza.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(arrazaPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    arrazaVM.Arraza.ImageUrl = @"\images\arraza\" + fileName;
                }
                if (arrazaVM.Arraza.ID == 0) 
                {
                    _UnitOfWork.Arraza.Add(arrazaVM.Arraza);
                }
                else
                {
                    _UnitOfWork.Arraza.Update(arrazaVM.Arraza);
                }
                _UnitOfWork.Save();
                TempData["success"] = "Arraza ondo sortuta";
                return RedirectToAction("Index");
            }
            else 
            {
                arrazaVM.LekuakList = _LekuaRepo.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Izena,
                    Value = u.ID.ToString()
                });
                return View(arrazaVM);
            }
            
        }



        public IActionResult Delete(int? ID)
        {
            if (ID == null)
            {
                //create
                return NotFound();
            }
            ArrazaVM arrazaVM = new()
            {
                LekuakList = _LekuaRepo.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Izena,
                    Value = u.ID.ToString()
                }),
                Arraza = new Arraza()
            };
 
            arrazaVM.Arraza = _UnitOfWork.Arraza.Get(u => u.ID == ID);
     
            if (arrazaVM == null)
            {
                return NotFound();
            }
            return View(arrazaVM);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? ID)
        {
            Arraza? obj = _UnitOfWork.Arraza.Get(u => u.ID == ID);
            if (obj == null)
            {
                return NotFound();
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (!string.IsNullOrEmpty(obj.ImageUrl))
            {
                //Delete the old image in the update
                var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _UnitOfWork.Arraza.Remove(obj);
            _UnitOfWork.Save();
            TempData["success"] = "Arraza ondo borratuta";
            return RedirectToAction("Index");

        }
    }
}


