using Gym_sports_training.Repository.DAL;
using Gym_sports_training.Repository.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace Gym_sports_training.Controllers.EntitiesControllers
{
    public class CoachesController : Controller
    {
        private ICoachRepository coachRepository;

        public CoachesController()
        {
            this.coachRepository = new CoachRepository(new GymContext());
        }

        // GET: Coaches
        public ActionResult Index(Speciality? coachSpeciality)
        {
            var coachSpecList = new List<Speciality?>();

            var coaches = from c in coachRepository.GetCoaches()
                          select c;

            var coachSpec = from c in coachRepository.GetCoaches()
                            orderby c.Speciality
                            select c.Speciality;

            coachSpecList.AddRange(coachSpec.Distinct());
            ViewBag.coachSpeciality = new SelectList(coachSpecList);

            if (coachSpeciality!=null)
            {
                coaches = coaches.Where(x => x.Speciality == coachSpeciality);
            }

            return View(coaches);
        }

        // GET: Coaches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = coachRepository.GetCoachByID(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // GET: Coaches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Coaches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LastName,Speciality,Price,TrainingLength,Description")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                coachRepository.InsertCoach(coach);
                coachRepository.Save();
                return RedirectToAction("Index");
            }

            return View(coach);
        }

        // GET: Coaches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = coachRepository.GetCoachByID(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LastName,Speciality,Price,TrainingLength,Description")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                coachRepository.UpdateCoach(coach);
                coachRepository.Save();
                return RedirectToAction("Index");
            }
            return View(coach);
        }

        // GET: Coaches/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            Coach coach = coachRepository.GetCoachByID(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Coach coach = coachRepository.GetCoachByID(id);
                coachRepository.DeleteCoach(id);
                coachRepository.Save();
            }
            catch
            {
                // Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                coachRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
