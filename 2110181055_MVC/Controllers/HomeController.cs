using _2110181055_MVC.Models;
using System.Linq;
using System.Web.Mvc;

namespace _2110181055_MVC.Controllers
{
    public class HomeController : Controller
    {
        private db_scholarsEntities db = new db_scholarsEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(university log)
        {
            using(db_scholarsEntities db = new db_scholarsEntities())
            {
                var university = db.universities.Where(u => u.email.Equals(log.email) && u.password.Equals(log.password)).FirstOrDefault();

                if (university != null)
                {
                    Session["university_id"] = (university.university_id).ToString();
                    Session["university_name"] = (university.university_name).ToString();
                    Session["email"] = (university.email).ToString();
                    return RedirectToAction("Index");
                }
                else
                {
                    log.LoginErrorMessage = "Wrong login credentials!";
                }
            }

            return View(log);
        }

        // GET: Universities/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Universities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "university_id,university_name,email,password,location")] university university)
        {
            if (ModelState.IsValid)
            {
                db.universities.Add(university);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(university);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}