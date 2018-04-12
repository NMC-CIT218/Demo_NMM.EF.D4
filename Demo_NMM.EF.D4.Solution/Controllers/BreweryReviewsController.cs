using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Demo_NMM.EF.Models;

namespace Demo_NMM.EF.D3.Solution.Controllers
{
    public class BreweryReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BreweryReviews
        public ActionResult Index()
        {
            return View(BuildBreweryReviewViewModelList(db.BreweryReviews.ToList()));
        }

        // list of reviews for a given brewery
        public ActionResult ListOfReviewsByBrewery(int Id)
        {
            var breweryReviews = db.BreweryReviews
                .Where(r => r.BreweryID == Id)
                .ToList();

            // get brewery to pass
            var brewery = db.Breweries.FirstOrDefault(b => b.ID == Id);
            ViewBag.Brewery = brewery;

            if (brewery != null)
            {
                return View(breweryReviews);
            }
            else
            {
                // redirect to error page with error message
                ViewBag.ErrorMessage = "Brewery not found.";
                return View("Error");
            }
        }

        // GET: BreweryReviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BreweryReview breweryReview = db.BreweryReviews.Find(id);
            BreweryReviewViewModel breweryReviewViewModel = BuildBreweryReviewViewModel(breweryReview);

            if (breweryReview == null)
            {
                return HttpNotFound();
            }
            return View(breweryReviewViewModel);
        }

        // GET: user create brewery review
        public ActionResult UserCreate()
        {
            return View();
        }

        // POST: user create brewery review
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreate([Bind(Include = "ID,DateCreated,Content,BreweryID")] BreweryReview breweryReview)
        {
            if (ModelState.IsValid)
            {
                db.BreweryReviews.Add(breweryReview);
                db.SaveChanges();
                return RedirectToAction("ListOfReviewsByBrewery", new { id = breweryReview.BreweryID });
            }

            return View(breweryReview);
        }

        // GET: BreweryReviews/Create
        public ActionResult Create()
        {
            // generate select list with ids for brewery dropdown
            var breweryList = db.Breweries.Select(b => b);
            ViewBag.SelectBreweryList = new SelectList(breweryList, "Id", "Name");

            return View();
        }

        // POST: BreweryReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateCreated,Content,BreweryID")] BreweryReview breweryReview)
        {
            if (ModelState.IsValid)
            {
                db.BreweryReviews.Add(breweryReview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(breweryReview);
        }

        // GET: BreweryReviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // generate select list with ids for brewery dropdown
            var breweryList = db.Breweries.Select(b => b);
            ViewBag.SelectBreweryList = new SelectList(breweryList, "Id", "Name");

            BreweryReview breweryReview = db.BreweryReviews.Find(id);
            BreweryReviewViewModel breweryReviewViewModel = BuildBreweryReviewViewModel(breweryReview);

            if (breweryReview == null)
            {
                return HttpNotFound();
            }
            return View(breweryReviewViewModel);
        }

        // POST: BreweryReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateCreated,Content,BreweryID")] BreweryReview breweryReview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(breweryReview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(breweryReview);
        }

        // GET: BreweryReviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BreweryReview breweryReview = db.BreweryReviews.Find(id);
            BreweryReviewViewModel breweryReviewViewModel = BuildBreweryReviewViewModel(breweryReview);

            if (breweryReview == null)
            {
                return HttpNotFound();
            }
            return View(breweryReviewViewModel);
        }

        // POST: BreweryReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BreweryReview breweryReview = db.BreweryReviews.Find(id);
            db.BreweryReviews.Remove(breweryReview);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Build out a BreweryReviewViewModel
        /// </summary>
        /// <param name="breweryReview">brewery review</param>
        /// <returns>brewery review ViewModel</returns>
        [NonAction]
        private BreweryReviewViewModel BuildBreweryReviewViewModel(BreweryReview breweryReview)
        {
            // generate a dictionary with brewery ids and names for lookup
            var breweryNames = db.Breweries.ToDictionary(b => b.ID, b => b.Name);

            return new BreweryReviewViewModel()
            {
                ID = breweryReview.ID,
                DateCreated = breweryReview.DateCreated,
                Content = breweryReview.Content,
                BreweryID = breweryReview.BreweryID,
                BreweryName = breweryNames[breweryReview.BreweryID]
            };
        }

        /// <summary>
        /// Build out a list of BreweryReviewViewModels from list of BreweryReviews
        /// </summary>
        /// <param name="breweryReviews"></param>
        /// <returns>list of brewery review ViewModels</returns>
        [NonAction]
        private List<BreweryReviewViewModel> BuildBreweryReviewViewModelList(List<BreweryReview> breweryReviews)
        {
            List<BreweryReviewViewModel> breweryReviewsViewModel = new List<BreweryReviewViewModel>();

            // generate a dictionary with brewery ids and names for lookup
            var breweryNames = db.Breweries.ToDictionary(b => b.ID, b => b.Name);

            foreach (var breweryReview in breweryReviews)
            {
                breweryReviewsViewModel.Add(new BreweryReviewViewModel
                {
                    ID = breweryReview.ID,
                    DateCreated = breweryReview.DateCreated,
                    Content = breweryReview.Content,
                    BreweryID = breweryReview.BreweryID,
                    BreweryName = breweryNames[breweryReview.BreweryID]
                });
            }
            return breweryReviewsViewModel;
        }
    }
}
