using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MessageController : Controller
    {
        private DB_StoreEntities db = new DB_StoreEntities();

        // GET: Message
        public ActionResult Index()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            Guid IdUser = new Guid(reqCookies["IdUser"]);
            if (db.TBUsers.Count(x=>x.IdUser== IdUser && x.role== "Administrator") >0)
            {
                var tBMsgs = db.TBMsgs.Include(t => t.TBUser);
                return View(tBMsgs.ToList());
            }
            else
            {
                return View();
            }
        }

        // GET: Message/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBMsg tBMsg = db.TBMsgs.Find(id);
            if (tBMsg == null)
            {
                return HttpNotFound();
            }
            return View(tBMsg);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TBMsg tBMsg = db.TBMsgs.Find(id);
            db.TBMsgs.Remove(tBMsg);
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
