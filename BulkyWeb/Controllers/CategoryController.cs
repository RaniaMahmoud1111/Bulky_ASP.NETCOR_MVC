using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
                _db = db;
        }
        public IActionResult Index()
        {
           List<Category> objCategoryList = _db.Categories.ToList();

            return View(objCategoryList);
        }

        public IActionResult Create() 
        {
        
        return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "the DisplayOrder cannot match the Name");
            }

            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("name", $"{obj.Name} is an invalid value");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
				TempData["success"] = "Category Created successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();
        }

	
		public IActionResult Edit(int? id)
		{
            if(id == null || id==0)
            {
                return NotFound();
            }
            Category? categoryFromDB=_db.Categories.Find(id);
		//	Category? categoryFromDB2 = _db.Categories.FirstOrDefault(u=>u.Id==id);
			//Category ?categoryFromDB3 = _db.Categories.Where(k=>k.Id==id).FirstOrDefault();


			if (categoryFromDB == null) 
            {
                return NotFound();
            }
			return View(categoryFromDB);
		}
        
		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "the DisplayOrder cannot match the Name");
			}

			if (obj.Name != null && obj.Name.ToLower() == "test")
			{
				ModelState.AddModelError("name", $"{obj.Name} is an invalid value");
			}

			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				TempData["success"]= "Category Updated successfully";
				return RedirectToAction("Index", "Category");
			}

			return View();
		}


		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? categoryFromDB = _db.Categories.Find(id);
			
			if (categoryFromDB == null)
			{
				return NotFound();
			}
			return View(categoryFromDB);
		}

		[HttpPost,ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Category? obj = _db.Categories.Find(id); 
			if (obj == null) 
			{
				return NotFound();
			}
				_db.Categories.Remove(obj);
				_db.SaveChanges();
			TempData["success"] = "Category Deleted successfully";

			return RedirectToAction("Index", "Category");
			
	
		}
	}
}
