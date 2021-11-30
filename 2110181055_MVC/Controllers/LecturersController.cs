using _2110181055_MVC.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace _2110181055_MVC.Controllers
{
    public class LecturersController : Controller
    {
        private db_scholarsEntities db = new db_scholarsEntities();

        // GET: lecturers
        public ActionResult Index()
        {
            if (Session["university_id"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int university_id = int.Parse(Session["university_id"].ToString());
                var lecturers = db.lecturers.Where(l => l.university_id == university_id).Include(l => l.faculty).Include(l => l.university);
                return View(lecturers.ToList());
            }
        }

        // GET: lecturers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lecturer lecturer = db.lecturers.Find(id);
            if (lecturer == null)
            {
                return HttpNotFound();
            }
            return View(lecturer);
        }

        // GET: lecturers/Create
        public ActionResult Create()
        {
            ViewBag.faculty_id = new SelectList(db.faculties, "faculty_id", "faculty_name");
            ViewBag.university_id = new SelectList(db.universities, "university_id", "university_name");
            return View();
        }

        // POST: lecturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "lecturer_id,faculty_id,lecturer_name,degree,gender")] lecturer lecturer)
        {
            lecturer.university_id = int.Parse(Session["university_id"].ToString());

            if (ModelState.IsValid)
            {
                db.lecturers.Add(lecturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.faculty_id = new SelectList(db.faculties, "faculty_id", "faculty_name", lecturer.faculty_id);
            ViewBag.university_id = new SelectList(db.universities, "university_id", "university_name", lecturer.university_id);
            return View(lecturer);
        }

        // GET: lecturers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lecturer lecturer = db.lecturers.Find(id);
            if (lecturer == null)
            {
                return HttpNotFound();
            }
            ViewBag.faculty_id = new SelectList(db.faculties, "faculty_id", "faculty_name", lecturer.faculty_id);
            ViewBag.university_id = new SelectList(db.universities, "university_id", "university_name", lecturer.university_id);
            return View(lecturer);
        }

        // POST: lecturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "lecturer_id,faculty_id,lecturer_name,degree,gender")] lecturer lecturer)
        {
            lecturer.university_id = int.Parse(Session["university_id"].ToString());

            if (ModelState.IsValid)
            {
                db.Entry(lecturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.faculty_id = new SelectList(db.faculties, "faculty_id", "faculty_name", lecturer.faculty_id);
            ViewBag.university_id = new SelectList(db.universities, "university_id", "university_name", lecturer.university_id);
            return View(lecturer);
        }

        // GET: lecturers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lecturer lecturer = db.lecturers.Find(id);
            if (lecturer == null)
            {
                return HttpNotFound();
            }
            return View(lecturer);
        }

        // POST: lecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            lecturer lecturer = db.lecturers.Find(id);
            db.lecturers.Remove(lecturer);
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
    }
}
