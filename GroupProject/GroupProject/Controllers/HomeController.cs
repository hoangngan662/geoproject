using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Spatial;
using GroupProject.Models;
using Newtonsoft.Json;
using GroupProject.Code;

namespace GroupProject.Controllers
{

    public class HomeController : Controller
    {
        Database db = new Database();
        public ActionResult Index()
        {
            var entity = db.Places.ToList();


            List<PlaceModel> listOfCords = new List<PlaceModel>();
            foreach (var item in entity)
            {
                PlaceModel model = new PlaceModel();
                model.name = item.name;
                model.keywords = item.keywords;
                model.cords = new double[] { item.location.YCoordinate ?? 0.0, item.location.XCoordinate ?? 0.0 };
                listOfCords.Add(model);
            }
            string json = JsonConvert.SerializeObject(listOfCords);
            ViewBag.json = json;
            ViewBag.model = db.Places.ToList();

            return View();
        }

        public ActionResult Test()
        {


            // Assume entity là một đối tượng trong cơ sở dữ liệu có một thuộc tính kiểu Geometry
            var entity = db.Places.ToList();


            List<PlaceModel> listOfCords = new List<PlaceModel>();
            foreach (var item in entity)
            {
                PlaceModel model = new PlaceModel();
                model.name = item.name;
                model.keywords = item.keywords;
                model.cords = new double[] { item.location.YCoordinate ?? 0.0, item.location.XCoordinate ?? 0.0 };
                listOfCords.Add(model);
            }
            ViewBag.listOfCords = listOfCords;
            string json = JsonConvert.SerializeObject(listOfCords);
            ViewBag.json = json;
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Trips(/*int id = 0*/)
        {
            //var entity = new List<Place>();
            //if(id == 0)
            //{
            var entity = db.Places.ToList();
            //}
            //else
            //{
            //    entity = db.Places.Where(p => p.id == id).ToList();
            //}



            List<PlaceModel> listOfCords = new List<PlaceModel>();
            foreach (var item in entity)
            {
                PlaceModel model = new PlaceModel();
                model.name = item.name;
                model.keywords = item.keywords;
                model.cords = new double[] { item.location.YCoordinate ?? 0.0, item.location.XCoordinate ?? 0.0 };
                listOfCords.Add(model);
            }
            string json = JsonConvert.SerializeObject(listOfCords);
            ViewBag.json = json;
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost, ActionName("Login")]
        public ActionResult Login(string username, string password)
        {
            if (username == "")
            {
                ModelState.AddModelError("UsernameLogin", "Tên đăng nhập không được để trống.");
                return View("Login");
            }
            if (password == "")
            {
                ModelState.AddModelError("PasswordLogin", "Mật khẩu không được để trống.");
                ViewBag.UsernameLogin = username;
                return View("Login");
            }

            var user = db.Accounts.SingleOrDefault(s => s.username == username && s.password == password);
            if (user != null)
            {
                SessionHelper.SetSession(new UserSession(user.username));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("PasswordLogin", "Tên đăng nhập hoặc mật khẩu không chính xác.");
                ViewBag.UsernameLogin = username;
            }
            return View("Login");
        }

        [HttpPost, ActionName("Register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Account model, string matkhau)
        {

            //kt email
            var checkUsername = db.Accounts.SingleOrDefault(s => s.username == model.username);
            if (checkUsername != null)
            {
                ModelState.AddModelError("Username", "Username đã được đăng ký.");
                return View("Login");
            }

            ViewBag.username = model.username;

            //kt mat khau
            if (matkhau.Length < 3 || matkhau.Length > 50)
            {
                ModelState.AddModelError("Password", "Mật khẩu phải từ 3 đến 50 ký tự");
                return View("Login");
            }

            if (matkhau == model.password)
            {
                if (ModelState.IsValid)
                {

                    db.Accounts.Add(model);
                    db.SaveChanges();
                    return View("Login");

                }
                else
                {
                    ModelState.AddModelError("", "Đăng ký không thành công, vui lòng kiểm tra lại");
                    return View("Login");
                }
            }
            else
            {
                ModelState.AddModelError("MatKhau", "Mật khẩu xác nhận không chính xác");
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("UserSession");
            return RedirectToAction("Index");
        }

        public ActionResult Post()
        {
            var posts = db.Posts.ToList();

            return View(posts);
        }
        public ActionResult CrPost()
        {
            return View();
        }

        [HttpPost, ActionName("CrPost")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CrPost(Post post)
        {
            post.create_time = DateTime.Now;
            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Post");
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}