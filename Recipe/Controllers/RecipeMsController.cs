using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipe.Models;

namespace Recipe.Controllers
{
    public class RecipeMsController : Controller
    {
        private readonly RecipeDBContext _context;

        public RecipeMsController(RecipeDBContext context)
        {
            _context = context;
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


        // GET: RecipeMs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecipeMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Ingredients,Steps,CookingTime,Category,UserId")] RecipeM recipeM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeMExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReview([Bind("Id,Comment,RecipeId,UserId")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = review.RecipeId });
            }

            // If the model state is invalid, re-fetch the associated recipe and return to the details view with the original view model
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
