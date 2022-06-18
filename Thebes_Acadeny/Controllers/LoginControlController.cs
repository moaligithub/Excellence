using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace myapp2.Controllers
{
    public class LoginControlController : Controller
    {
        private readonly IUnitOfWork<Admin> _admin;
        private readonly IUnitOfWork<MessagesUser> _message;
        private readonly IUnitOfWork<Books> _book;
        private readonly IUnitOfWork<Exam> _exam;
        private readonly IUnitOfWork<Posts> _post;
        private readonly IUnitOfWork<Video> _video;

        public LoginControlController(IUnitOfWork<Admin> admin , IUnitOfWork<MessagesUser> message ,
                                      IUnitOfWork<Books> book , IUnitOfWork<Exam> exam , 
                                      IUnitOfWork<Posts> post , IUnitOfWork<Video> video)
        {
            _admin = admin;
            _message = message;
            _book = book;
            _exam = exam;
            _post = post;
            _video = video;
        }


        public IActionResult ControlAdmin()
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                Admin admin = _admin.Entity.GetById(HttpContext.Session.GetInt32("IdAdmin").Value);
                return View(admin);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ControlAdminShimaa()
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                Admin admin = _admin.Entity.GetById(HttpContext.Session.GetInt32("IdAdmin").Value);
                return View(admin);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ControlOwner112004()
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                ViewBag.MeesageCount = _message.Entity.GetAll().Where(m => m.Bol == false).Count();
                
                int book = _book.Entity.GetAll().Where(b => b.Assent == false).Count();
                int exam = _exam.Entity.GetAll().Where(e => e.Assent == false).Count();
                int video = _video.Entity.GetAll().Where(v => v.Assent == false).Count();
                int post = _post.Entity.GetAll().Where(p => p.Assent == false).Count();

                ViewBag.request = (book + exam + video + post);
                
                Admin admin = _admin.Entity.GetById(HttpContext.Session.GetInt32("IdAdmin").Value);
                return View(admin);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult LoginAdmin()
        {
            Admin model = _admin.Entity.GetAll().Where(a => a.GategoryIdFk == 1).SingleOrDefault();

            return View(model);
        }

        [HttpPost]
        public IActionResult LoginAdmin(string username , string pass)
        {
            Admin ad = _admin.Entity.GetAll().Where(p => p.Password == pass && p.UserName == username && p.GategoryIdFk == 3).SingleOrDefault();

            if (ad == null)
            {
                ViewBag.Error = "اسم المستخدم او كلمه السر خاطئه";

                Admin model = _admin.Entity.GetAll().Where(a => a.GategoryIdFk == 1).SingleOrDefault();
                return View("LoginAdmin", model);
            }
            else
            {

                HttpContext.Session.SetInt32("IdAdmin", ad.AdminId);

                return RedirectToAction(nameof(ControlAdmin));
            }

        }

        [HttpGet]
        public IActionResult LoginAdminShimaa()
        {
            Admin model = _admin.Entity.GetAll().Where(a => a.GategoryIdFk == 1).SingleOrDefault();

            return View(model);
        }

        [HttpPost]
        public IActionResult LoginAdminShimaa(string username, string pass)
        {
            Admin ad = _admin.Entity.GetAll().Where(p => p.Password == pass && p.UserName == username && p.GategoryIdFk == 2).SingleOrDefault();

            if (ad == null)
            {
                ViewBag.Error = "اسم المستخدم او كلمه السر خاطئه";

                Admin model = _admin.Entity.GetAll().Where(a => a.GategoryIdFk == 1).SingleOrDefault();
                return View("LoginAdminShimaa", model);
            }
            else
            {
                HttpContext.Session.SetInt32("IdAdmin" , ad.AdminId);

                return RedirectToAction(nameof(ControlAdminShimaa));
            }

        }

        [HttpGet]
        public IActionResult LoginOwner112004()
        {
            Admin model = _admin.Entity.GetAll().Where(a => a.GategoryIdFk == 1).SingleOrDefault();

            return View(model);
        }

        [HttpPost]
        public IActionResult LoginOwner112004(string username, string pass)
        {
            Admin Ow = _admin.Entity.GetAll().Where(p => p.Password == pass && p.UserName == username && p.GategoryIdFk == 1).SingleOrDefault();

            if (Ow == null)
            {
                ViewBag.Error = "اسم المستخدم او كلمه السر خاطئه";

                Admin model = _admin.Entity.GetAll().Where(a => a.GategoryIdFk == 1).SingleOrDefault();
                return View("LoginOwner112004", model);
            }
            else
            {
                HttpContext.Session.SetInt32("IdAdmin", Ow.AdminId);

                return RedirectToAction(nameof(ControlOwner112004));
            }

        }

    }
}
