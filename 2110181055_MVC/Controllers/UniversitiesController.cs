using _2110181055_MVC.Models;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;

namespace _2110181055_MVC.Controllers
{
    public class UniversitiesController : Controller
    {
        private db_scholarsEntities db = new db_scholarsEntities();

        // GET: Universities
        public ActionResult Index()
        {
            if (Session["university_id"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            university university = db.universities.Find(int.Parse(Session["university_id"].ToString()));
            if (university == null)
            {
                return HttpNotFound();
            }
            return View(university);
        }

        // GET: Universities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            university university = db.universities.Find(id);
            if (university == null)
            {
                return HttpNotFound();
            }
            return View(university);
        }

        // POST: Universities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "university_id,university_name,email,password,location")] university university)
        {
            if (ModelState.IsValid)
            {
                db.Entry(university).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(university);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
