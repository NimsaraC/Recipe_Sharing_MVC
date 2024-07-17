using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipe.Models;

namespace Recipe.Controllers
{
    public class RecipeMsController : Controller
    {
        private readonly RecipeDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RecipeMsController(RecipeDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: RecipeMs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recipes.ToListAsync());
        }

        // GET: RecipeMs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var review = GetReviewIdAsync(id);

            var recipeM = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);
            if (recipeM == null)
            {
                return NotFound();
            }

            var viewModel = new RecipeDetails
            {
                RecipeM = recipeM,
                Reviews = review,
                Review = new Review { RecipeId = recipeM.Id }
            };

            return View(viewModel);
        }

        public async Task<IActionResult> DetailsUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var review = GetReviewIdAsync(id);

            var recipeM = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);
            if (recipeM == null)
            {
                return NotFound();
            }

            var viewModel = new RecipeDetails
            {
                RecipeM = recipeM,
                Reviews = review,
                Review = new Review { RecipeId = recipeM.Id }
            };

            return View(viewModel);
        }

        public IEnumerable<Review> GetReviewIdAsync(int? id)
        {
            return _context.Reviews.Where(r => r.RecipeId == id).ToList();
        }


        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecipeMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Ingredients,Steps,CookingTime,Category,UserId,ImageFileName")] RecipeDTO recipeM)
        {
            if (ModelState.IsValid)
            {
                if (recipeM.ImageFileName != null && recipeM.ImageFileName.Length > 0)
                {
                    // Handle file upload and save to disk
                    string newFilename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(recipeM.ImageFileName.FileName);
                    string imagefullPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", newFilename);

                    using (var stream = new FileStream(imagefullPath, FileMode.Create))
                    {
                        await recipeM.ImageFileName.CopyToAsync(stream);
                    }

                    // Create RecipeM entity to save in database
                    var recipeEntity = new RecipeM
                    {
                        Title = recipeM.Title,
                        Description = recipeM.Description,
                        Ingredients = recipeM.Ingredients,
                        Steps = recipeM.Steps,
                        CookingTime = recipeM.CookingTime,
                        Category = recipeM.Category,
                        UserId = recipeM.UserId,
                        ImageFileName = newFilename
                    };

                    _context.Add(recipeEntity);
                    await _context.SaveChangesAsync();

                    ViewBag.Message = $"Successfully created the recipe.";
                    return View();
                }
                else
                {
                    // Handle case where ImageFileName is null or empty
                    ModelState.AddModelError("ImageFileName", "Please choose a file for the recipe image.");
                }
            }

            // If ModelState is invalid, return the view with the model to display validation errors
            return View(recipeM);
        }




        // GET: RecipeMs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeM = await _context.Recipes.FindAsync(id);
            if (recipeM == null)
            {
                return NotFound();
            }
            return View(recipeM);
        }

        // POST: RecipeMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Ingredients,Steps,CookingTime,Category,UserId")] RecipeM recipeM)
        {
            if (id != recipeM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeMExists(recipeM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipeM);
        }

        // GET: RecipeMs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeM = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeM == null)
            {
                return NotFound();
            }
            var userId = recipeM.UserId; // or however you get the UserId
            ViewBag.UserId = userId;
            return View(recipeM);
        }

        // POST: RecipeMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipeM = await _context.Recipes.FindAsync(id);
            if (recipeM != null)
            {
                _context.Recipes.Remove(recipeM);
            }

            await _context.SaveChangesAsync();
            ViewBag.Message = $"Item Deleted Successfully";
            return View();
        }

        private bool RecipeMExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReview([Bind("Id,Comment,RecipeId,UserName")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = review.RecipeId });
            }

            var recipeM = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == review.RecipeId);
            if (recipeM == null)
            {
                return NotFound();
            }

            var viewModel = new RecipeDetails
            {
                RecipeM = recipeM,
                Review = review
            };

            return View("Details", viewModel);
        }

    }
}
