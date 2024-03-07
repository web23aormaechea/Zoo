using Microsoft.AspNetCore.Mvc;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers
{
    public class ArrazaController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _db;
        public ArrazaController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Arraza> objArrazaList = _db.Arrazak.ToList();
            return View(objArrazaList);
        }
        public IActionResult List()
        {
            List<Arraza> objArrazaList = _db.Arrazak.ToList();
            return View(objArrazaList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Arraza obj, IFormFile? file)
        {
            obj.ImageUrl = "temp";
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file!= null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string arrazaPath = Path.Combine(wwwRootPath, @"images\arraza");

                    using (var fileStream = new FileStream(Path.Combine(arrazaPath, fileName),FileMode.Create)) 
                    { 
                        file.CopyTo(fileStream);
                    }
                    obj.ImageUrl = @"\images\arraza\" + fileName;
                }
                _db.Arrazak.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Arraza ondo sortuta";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Arraza? arrazaFromDb = _db.Arrazak.Find(Id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==Id);
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == Id).FirstOrDefault();
            if (arrazaFromDb == null)
            {
                return NotFound();
            }
            return View(arrazaFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Arraza obj)
        {

            if (ModelState.IsValid)
            {
                _db.Arrazak.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Arraza ondo aldatuta";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Arraza? categoryFromDb = _db.Arrazak.Find(Id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Arraza? obj = _db.Arrazak.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Arrazak.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Arraza ondo borratuta";
            return RedirectToAction("Index");

        }
    }
}


