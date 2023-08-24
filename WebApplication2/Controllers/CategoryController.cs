using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            List<Category> objCategoryList=_db.Categories.ToList();
            return View(objCategoryList);
        }
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db= db;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj) //getting data from category model
        {
           if (obj.Name==obj.DisplayOrder.ToString())
            {
               ModelState.AddModelError("name", "The display order cannot exactly match the name");
            }
            //if (obj.Name!=null && obj.Name.ToLower() == "test")
            //{
              //  ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj); // what is to be add
                _db.SaveChanges(); //go to the database and create category
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index"); //view all categories in index() line 9;
                                                  //return RedirectToAction("Index","Category");
                                                  //-> if we have different controller then add like
                                                  //this else keep only menthod/function name
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(c=>c.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (categoryFromDb == null) 
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj) //getting data from category model
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order cannot exactly match the name");
            }
            //if (obj.Name!=null && obj.Name.ToLower() == "test")
            //{
            //  ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj); // what is to be add
                _db.SaveChanges(); //go to the database and create category
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index"); //view all categories in index() line 9;
                                                  //return RedirectToAction("Index","Category");
                                                  //-> if we have different controller then add like
                                                  //this else keep only menthod/function name
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(c=>c.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id) //getting data from category model
        {
            Category? obj= _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj); 
            _db.SaveChanges(); //go to the database and create category
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index"); //view all categories in index() line 9;
                                                  //return RedirectToAction("Index","Category");
                                                  //-> if we have different controller then add like
                                                  //this else keep only menthod/function name            
        }
    }
}
