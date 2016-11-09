using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using PagedList;
using Gym_sports_training.Repository.DAL;
using Gym_sports_training.Repository.Models;

namespace Gym_sports_training.Controllers.EntitiesControllers
{
    public class ClientsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        
        // GET: Clients
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var sortedClients = from s in unitOfWork.ClientRepository.Get()
                                select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                int flag = 0;
                int temp;
                bool test = Int32.TryParse(searchString, out temp);
                if (test)
                    flag = 1;

                if (searchString.Contains("@"))
                    flag = 2;

                switch (flag)
                {
                    case 0:
                        sortedClients = from s in unitOfWork.ClientRepository.Get()
                                        where s.LastName.Contains(searchString)
                                        select s;
                        break;
                    case 1:
                        sortedClients = from s in unitOfWork.ClientRepository.Get()
                                        where s.PhoneNumber.Contains(searchString)
                                        select s;
                        break;
                    case 2:
                        sortedClients = from s in unitOfWork.ClientRepository.Get()
                                        where s.EMail.Contains(searchString)
                                        select s;
                        break;
                }

            }

            switch (sortOrder)
            {
                case "name_desc":
                    sortedClients = sortedClients.OrderByDescending(s => s.LastName);
                    break;
                default:  
                    sortedClients = sortedClients.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(sortedClients.ToPagedList(pageNumber, pageSize));
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = unitOfWork.ClientRepository.GetByID(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LastName,EMail,PhoneNumber")] Client client)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.ClientRepository.Insert(client);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = unitOfWork.ClientRepository.GetByID(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LastName,EMail,PhoneNumber")] Client client)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.ClientRepository.Update(client);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
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

            Client client = unitOfWork.ClientRepository.GetByID(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Client client = unitOfWork.ClientRepository.GetByID(id);
                unitOfWork.ClientRepository.Delete(client);
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
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
