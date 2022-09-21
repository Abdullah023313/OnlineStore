using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DefaultController : Controller
    {
        DB_StoreEntities db = new DB_StoreEntities();
        HttpCookie reqCookies;
        public ActionResult login()
        {
            reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("login");
        }
        [HttpPost]
        public ActionResult login(string mail, string password, string Rememberme)
        {
            try
            {
                if (db.TBUsers.Count(c => c.password == password && c.mail == mail) == 1)
                {
                    HttpCookie userInfo = new HttpCookie("userInfo");

                    userInfo["Mail"] = mail.ToLower();
                    userInfo["password"] = password;
                    userInfo["IdUser"] = db.TBUsers.Where(u => u.mail == mail && u.password == password).Select(u => u.IdUser).SingleOrDefault().ToString();
                    userInfo.Expires = DateTime.Now.AddYears(10);
                    Response.Cookies.Add(userInfo);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["error"] = "Error - email or password is incorrect";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(string username, string mail, string password, string Confirmpassword)
        {
            try
            {
                int count_N = db.TBUsers.Count(n => n.mail == mail);
                int count_M = db.TBUsers.Count(n => n.username == username);
                if (Confirmpassword == password && password.Length >= 6 && count_M == 0 && count_N == 0)
                {
                    Guid IdUser = Guid.NewGuid();
                    db.addUser(IdUser, username, mail.ToLower(), "Customer", password);
                    HttpCookie userInfo = new HttpCookie("userInfo");
                    userInfo["Mail"] = mail.ToLower();
                    userInfo["password"] = password;
                    userInfo["IdUser"] = IdUser.ToString();
                    userInfo.Expires = DateTime.Now.AddYears(10);
                    Response.Cookies.Add(userInfo);

                    return RedirectToAction("Index");
                }
                else
                {
                    if (count_N > 0)
                    {
                        ViewData["name"] = "Name already exist";

                    }
                    if (count_M > 0)
                    {
                        ViewData["Mail"] = "Email already exist";

                    }
                    if (Confirmpassword != password)
                    {
                        ViewData["error"] = "check the passwoed";

                    }
                    if (password.Length < 6)
                    {
                        ViewData["password"] = "Weak passwoed";

                    }
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Index()
        {
            dynamic dy = new ExpandoObject();
            dy.productlist = db.TBProducts.Where(x => x.quantity > 0 && x.show == true).ToList();
            dy.Imagelist = db.getMainImage().ToList();
            dy.Colorlist = db.TBColors.ToList();
            reqCookies = Request.Cookies["userInfo"];
            Guid IdUser = new Guid(reqCookies["IdUser"]);
            dy.numberProducts = db.TBCarts.Where(x => x.IdUser == IdUser).Sum(x => x.quantity).ToString(); //numberOfProductsInTheBasket
            dy.ProductTypeslist = db.TBProductTypes.ToList();
            return View(dy);

        }

        [HttpPost]
        public ActionResult Index(string search)
        {
            dynamic dy = new ExpandoObject();

            dy.productlist = db.TBProducts.Where(a => (a.quantity > 0 && a.show == true) && (a.name.Contains(search) ||
            a.details.Contains(search) ||
            a.company.Contains(search) ||
            a.description.Contains(search) ||
            a.TBProductType.type.Contains(search) ||
            a.TBProductType.IdType.ToString() == search ||
            a.sellingPrice.ToString().Contains(search)));

            dy.Imagelist = db.getMainImage().ToList();
            dy.Colorlist = db.TBColors.ToList();
            dy.ProductTypeslist = db.TBProductTypes.ToList();
            reqCookies = Request.Cookies["userInfo"];
            Guid IdUser = new Guid(reqCookies["IdUser"]);
            dy.numberProducts = db.TBCarts.Where(x => x.IdUser == IdUser).Sum(x => x.quantity).ToString(); //numberOfProductsInTheBasket

            return View(dy);

        }
        public ActionResult Cart()
        {
            reqCookies = Request.Cookies["userInfo"];
            Guid IdUser = new Guid(reqCookies["IdUser"]);
            dynamic dy = new ExpandoObject();
            db.checkQuantitiesForUser(IdUser);
            dy.product = db.getProductsCart(IdUser).ToList();
            dy.FinalPrice = db.FinalPriceCart(IdUser).SingleOrDefault();
            dy.moreproducts = db.getmoreProduct(IdUser).ToList();
            return View(dy);
        }
        public ActionResult addCart(Guid IdProduct)
        {
            reqCookies = Request.Cookies["userInfo"];
            Guid Id = Guid.NewGuid();
            db.addProductToCart(Id, new Guid(reqCookies["IdUser"]), IdProduct);
            return RedirectToAction("index");
        }
        public ActionResult UpdateCart(Guid IdProduct, int gty)
        {
            reqCookies = Request.Cookies["userInfo"];
            db.UpdateCart(new Guid(reqCookies["IdUser"]), IdProduct, gty);
            return RedirectToAction("Cart");
        }
        public ActionResult deleteProductCart(Guid IdProduct)
        {
            reqCookies = Request.Cookies["userInfo"];
            db.deleteProductCart(new Guid(reqCookies["IdUser"]), IdProduct);
            return RedirectToAction("Cart");
        }
        [HttpPost]
        public ActionResult msg(string name, string mail, string Phone, string Subject, string msg)
        {
            Guid Idmsg = Guid.NewGuid();
            reqCookies = Request.Cookies["userInfo"];
            Guid IdUser = new Guid(reqCookies["IdUser"].ToString());
            db.addmsg(Idmsg, msg, name, Phone, mail, Subject, IdUser, DateTime.Now);//error Idmsg
            return RedirectToAction("Index");
        }

        // GET: Default/Details/5
        public ActionResult Details(Guid id)
        {
            dynamic product = new ExpandoObject();
            product.item = db.TBProducts.Find(id);
            product.productImages = db.TBImages.Where(x => x.IdProduct == id).ToList();
            TBImage image = db.TBImages.Where(x => x.IdProduct == id).FirstOrDefault();
            if (image != null)
            {
                ViewBag.mainImg = image.pathImage;
            }
            else
            {
                ViewBag.mainImg = "question.png";
            }
            product.productColors = db.TBColors.Where(x => x.IdProduct == id).ToList();
            reqCookies = Request.Cookies["userInfo"];
            product.countRate = db.TBRates.Where(x => x.IdProduct == id).Count();
            product.x = true;
            if (db.User_rating(id, new Guid(reqCookies["IdUser"].ToString())).Single() != 0)
            {
                product.x = false;
            }

            return View(product);
        }
        public ActionResult rate(Guid IdProduct)
        {
            reqCookies = Request.Cookies["userInfo"];
            Guid IdUser = new Guid(reqCookies["IdUser"]);
            if (db.TBRates.Count(x => x.IdUser == IdUser && x.IdProduct == IdProduct) == 0)
            {
                return View(IdProduct);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult rate(int rate, string IdProduct)
        {
            try
            {
                TBRate r = new TBRate();
                reqCookies = Request.Cookies["userInfo"];
                r.IdUser = new Guid(reqCookies["IdUser"].ToString());
                r.IdRate = new Guid();
                r.rate = rate;
                r.IdRate = Guid.NewGuid();
                r.IdProduct = new Guid(IdProduct);
                db.TBRates.Add(r);
                db.SaveChanges();
                db.ModifyRate(r.IdProduct);
                return RedirectToAction("Details", new { id = r.IdProduct });
            }
            catch
            {
                return View();
            }
        }
    }
}