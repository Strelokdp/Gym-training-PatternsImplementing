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
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Coaches
        public ActionResult Index(Speciality? coachSpeciality)
        {
            var coachSpecList = new List<Speciality?>();

            var coaches = from c in unitOfWork.CoachRepository.Get()
                          select c;

            var coachSpec = from c in unitOfWork.CoachRepository.Get()
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
            Coach coach = unitOfWork.CoachRepository.GetByID(id);
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
                unitOfWork.CoachRepository.Insert(coach);
                unitOfWork.Save();
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
            Coach coach = unitOfWork.CoachRepository.GetByID(id);
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
                unitOfWork.CoachRepository.Update(coach);
                unitOfWork.Save();
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

            Coach coach = unitOfWork.CoachRepository.GetByID(id);
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
                Coach coach = unitOfWork.CoachRepository.GetByID(id);
                unitOfWork.CoachRepository.Delete(coach);
                unitOfWork.Save();
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
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
