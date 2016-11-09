using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Gym_sports_training.Repository.DAL;
using Gym_sports_training.Repository.Models;
using PagedList;

namespace Gym_sports_training.Controllers.EntitiesControllers
{
    public class TrainingSessionsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: TrainingSessions
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PhoneSortParm = String.IsNullOrEmpty(sortOrder) ? "phone_desc" : "";
            ViewBag.TrainingStartSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var trainingSessions = unitOfWork.TrainingSessionRepository.Get(includeProperties: "Coach, Client");

            if (!String.IsNullOrEmpty(searchString))
            {
                trainingSessions = trainingSessions.Where(s => s.Client.PhoneNumber.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "phone_desc":
                    trainingSessions = trainingSessions.OrderByDescending(s => s.Client.PhoneNumber);
                    break;
                case "Date":
                    trainingSessions = trainingSessions.OrderBy(s => s.TrainingTimeStart);
                    break;
                case "date_desc":
                    trainingSessions = trainingSessions.OrderByDescending(s => s.TrainingTimeStart);
                    break;
                default:
                    trainingSessions = trainingSessions.OrderBy(s => s.Client.PhoneNumber);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(trainingSessions.ToPagedList(pageNumber, pageSize));
        }

        public RedirectResult CoachDetails(int? id)
        {
            return Redirect("http://localhost:61467/Coaches/Details/" + id);
        }

        // GET: TrainingSessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingSession trainingSession = unitOfWork.TrainingSessionRepository.GetByID(id);
            if (trainingSession == null)
            {
                return HttpNotFound();
            }
            return View(trainingSession);
        }

        // GET: TrainingSessions/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(unitOfWork.ClientRepository.Get(), "Id", "Name");
            ViewBag.CoachId = new SelectList(unitOfWork.CoachRepository.Get(), "Id", "Name");
            return View();
        }

        // POST: TrainingSessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientId,CoachId,TrainingTimeStart")] TrainingSession trainingSession)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.TrainingSessionRepository.Insert(trainingSession);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(unitOfWork.ClientRepository.Get(), "Id", "Name", trainingSession.ClientId);
            ViewBag.CoachId = new SelectList(unitOfWork.CoachRepository.Get(), "Id", "Name", trainingSession.CoachId);
            return View(trainingSession);
        }

        // GET: TrainingSessions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingSession trainingSession = unitOfWork.TrainingSessionRepository.GetByID(id);
            if (trainingSession == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(unitOfWork.ClientRepository.Get(), "Id", "Name", trainingSession.ClientId);
            ViewBag.CoachId = new SelectList(unitOfWork.CoachRepository.Get(), "Id", "Name", trainingSession.CoachId);
            return View(trainingSession);
        }

        // POST: TrainingSessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientId,CoachId,TrainingTimeStart")] TrainingSession trainingSession)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.TrainingSessionRepository.Update(trainingSession);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(unitOfWork.ClientRepository.Get(), "Id", "Name", trainingSession.ClientId);
            ViewBag.CoachId = new SelectList(unitOfWork.CoachRepository.Get(), "Id", "Name", trainingSession.CoachId);
            return View(trainingSession);
        }

        // GET: TrainingSessions/Delete/5
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

            TrainingSession trainingSession = unitOfWork.TrainingSessionRepository.GetByID(id);
            if (trainingSession == null)
            {
                return HttpNotFound();
            }
            return View(trainingSession);
        }

        // POST: TrainingSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                TrainingSession trainingSession = unitOfWork.TrainingSessionRepository.GetByID(id);
                unitOfWork.TrainingSessionRepository.Delete(trainingSession);
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
