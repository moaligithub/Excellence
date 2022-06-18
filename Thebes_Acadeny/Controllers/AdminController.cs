using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;
using Thebes_Acadeny.Models.Entity;

namespace Thebes_Acadeny.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork<Admin> _aadmin;
        private readonly IUnitOfWork<Specialties> _sspecialries;
        private readonly IUnitOfWork<Level> _llevel;
        private readonly IUnitOfWork<MessagesUser> userMessage;
        private readonly IHostingEnvironment _hosting;
        private readonly IUnitOfWork<Categores> _categores;
        private readonly IUnitOfWork<Books> _books;
        private readonly IUnitOfWork<Posts> _posts;
        private readonly IUnitOfWork<Video> _video;
        private readonly IUnitOfWork<Exam> _exam;
        private readonly IUnitOfWork<Plant> _plant;
        private readonly IUnitOfWork<UrlWebSite> _url;

        public AdminController(IUnitOfWork<Admin> aadmin,IUnitOfWork<Specialties> sspecialries,
                              IUnitOfWork<Level> llevel, IUnitOfWork<MessagesUser> UserMessage,
                              IHostingEnvironment hosting , IUnitOfWork<Categores> categores ,
                              IUnitOfWork<Books> books , IUnitOfWork<Posts> posts , 
                              IUnitOfWork<Video> video , IUnitOfWork<Exam> exam , 
                              IUnitOfWork<Plant> plant , IUnitOfWork<UrlWebSite> url)
        {
            _aadmin = aadmin;
            _sspecialries = sspecialries;
            _llevel = llevel;
            userMessage = UserMessage;
            _hosting = hosting;
            _categores = categores;
            _books = books;
            _posts = posts;
            _video = video;
            _exam = exam;
            _plant = plant;
            _url = url;
        }

        // GET: AdminController
       
        // GET: AdminController/Profile/5
        public ActionResult Profile(int id)
        {
            ProfileViewModel<Admin> model = new ProfileViewModel<Admin>();
            
            ProfileMethod(model, id);
            
            return View(model);
        }
        
        public ActionResult Index()
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                IndexAdminViewModel Items = new IndexAdminViewModel();
                return View(IndexView(Items));
            }else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        public ActionResult Index2()
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                IndexAdminViewModel Items = new IndexAdminViewModel();
                return View(IndexView2(Items));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: AdminController/Create
        public ActionResult Cre()
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                AddAdminViewModel viewModel = new AddAdminViewModel();

                return View(AddAdmin(viewModel));
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Cre2()
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                AddAdminViewModel viewModel = new AddAdminViewModel();

                return View(AddAdmin(viewModel));
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cre(AddAdminViewModel Model)
        {
            try
            {
                if(Model.SpeId == -1 || Model.LevId == -1)
                {
                   

                    if(Model.LevId == -1)
                    {
                        ViewBag.ErrorLev = "لم تقم بادخال المستوي بعد";
                    }
                    if(Model.SpeId == -1)
                    {
                        ViewBag.ErrorSpe = "لم تقم بادخال التخصص بعد";
                    }
                    AddAdminViewModel model = new AddAdminViewModel();
                    return View(AddAdmin(model));
                }
                else
                {
                    Admin newAdmin = new Admin()
                    {
                        FullName = Model.FullName,
                        ImageUrl = UplodedFile(Model.File, @"Tmb1/img/Admins"),
                        UserName = Model.UserName,
                        Password = Model.Password,
                        LevelIdFk = Model.LevId,
                        SpecialtiesId = Model.SpeId,
                        PhoneNumper = Model.PhoneNumber,
                        WhatsApp = "Https://api.whatsapp.com/send?phone=02"+Model.PhoneNumber,
                        GategoryIdFk = 3
                    };

                 

                    _aadmin.Entity.Insert(newAdmin);
                    _aadmin.Save();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cre2(AddAdminViewModel Model)
        {
            try
            {  
                Admin newAdmin = new Admin()
                {
                    FullName = Model.FullName,
                    ImageUrl = UplodedFile(Model.File, @"Tmb1/img/Admins"),
                    UserName = Model.UserName,
                    Password = Model.Password,
                    PhoneNumper = Model.PhoneNumber,
                    WhatsApp = "Https://api.whatsapp.com/send?phone=02" + Model.PhoneNumber,
                    GategoryIdFk = 2,
                    SpecialtiesId = -1,
                    LevelIdFk = -1
                };



                _aadmin.Entity.Insert(newAdmin);
                _aadmin.Save();

                return RedirectToAction(nameof(Index2));
                
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null) 
            {
                AddAdminViewModel viewModel = new AddAdminViewModel();
                return View(EditAdmin(viewModel, id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Edit2(int id)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                AddAdminViewModel viewModel = new AddAdminViewModel();
                return View(EditAdmin(viewModel, id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: AdminController/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddAdminViewModel viewModel)
        {
            try
            {
                string FileName = UplodedFile(viewModel.File, @"Tmb1/img/Admins");

                Admin newAd = new Admin()
                {
                    FullName = viewModel.FullName,
                    UserName = viewModel.UserName,
                    Password = viewModel.Password,
                    PhoneNumper = viewModel.PhoneNumber,
                    WhatsApp = "Https://api.whatsapp.com/send?phone=02"+viewModel.PhoneNumber,
                    LevelIdFk = viewModel.LevId,
                    SpecialtiesId = viewModel.SpeId,
                    AdminId = viewModel.AdId,
                    GategoryIdFk = viewModel.cateId
                };

                if(FileName != null)
                {
                    newAd.ImageUrl = FileName;
                    DeleteFile(viewModel.ImageUrl, @"Tmb1/img/Admins");
                }
                else
                {
                    newAd.ImageUrl = viewModel.ImageUrl;
                }
                
                _aadmin.Entity.UpDate(newAd);
                _aadmin.Save();

                return RedirectToAction("Index" , "Home");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2(AddAdminViewModel viewModel)
        {
            try
            {
                string FileName = UplodedFile(viewModel.File, @"Tmb1/img/Admins");

                Admin newAd = new Admin()
                {
                    FullName = viewModel.FullName,
                    UserName = viewModel.UserName,
                    Password = viewModel.Password,
                    PhoneNumper = viewModel.PhoneNumber,
                    WhatsApp = "Https://api.whatsapp.com/send?phone=02" + viewModel.PhoneNumber,
                    AdminId = viewModel.AdId,
                    LevelIdFk = viewModel.LevId,
                    SpecialtiesId = viewModel.SpeId,
                    GategoryIdFk = viewModel.cateId
                };

                if (FileName != null)
                {
                    newAd.ImageUrl = FileName;
                    DeleteFile(viewModel.ImageUrl, @"Tmb1/img/Admins");
                }
                else
                {
                    newAd.ImageUrl = viewModel.ImageUrl;
                }

                _aadmin.Entity.UpDate(newAd);
                _aadmin.Save();

                return RedirectToAction("Index" , "Home");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
           
            if(id != -1)
            {
                Admin ad = _aadmin.Entity.GetById(id);
                _aadmin.Entity.Delete(id);  
                _aadmin.Save();
                if(ad.ImageUrl != null)
                {
                    DeleteFile(ad.ImageUrl, @"Tmb1/img/Admins");
                }
            }

            return RedirectToAction(nameof(Index));
             
        }

        public ActionResult Delete2(int id)
        {

            if (id != -1)
            {
                Admin ad = _aadmin.Entity.GetById(id);
                _aadmin.Entity.Delete(id);
                _aadmin.Save();
                if (ad.ImageUrl != null)
                {
                    DeleteFile(ad.ImageUrl, @"Tmb1/img/Admins");
                }
            }
            return RedirectToAction(nameof(Index2));
        }

        public ActionResult Order()
        {
            OrderViewModel viewModel = new OrderViewModel();

            try
            {
                viewModel.Exams = _exam.Entity.GetAll().Where(e => e.Assent == false).ToList();
            }
            catch
            {
                viewModel.Exams = null;
            }
            try
            {
                viewModel.Plants = _plant.Entity.GetAll().ToList();
            }
            catch
            {
                viewModel.Plants = null;
            }
            try
            {
                viewModel.Posts = _posts.Entity.GetAll().Where(p => p.Assent == false).ToList();
            }
            catch
            {
                viewModel.Posts = null;
            }
            try
            {
                viewModel.WebSiteUrl = _url.Entity.GetAll().FirstOrDefault().UrlText;
            }
            catch
            {
                viewModel.WebSiteUrl = null;
            }
            try
            {
                viewModel.Videos = _video.Entity.GetAll().Where(v => v.Assent == false).ToList();
            }
            catch
            {
                viewModel.Videos = null;
            }
            try
            {
                viewModel.Admins = _aadmin.Entity.GetAll().ToList();
            }
            catch
            {
                viewModel.Admins = null;
            }
            try
            {
                viewModel.Books = _books.Entity.GetAll().Where(b => b.Assent == false).ToList();
            }
            catch
            {
                viewModel.Books = null;
            }

            return View(viewModel);
        }

        //this Method........
        public void ProfileMethod(ProfileViewModel<Admin> model , int Id)
        {
            try
            {
                model.Adm = _aadmin.Entity.GetById(Id);
            }
            catch
            {
                model.Adm = null;
            }
            try
            {
                model.Level = _llevel.Entity.GetById(model.Adm.LevelIdFk);
            }
            catch
            {
                model.Level = null;
            }
            try
            {
                model.Specialties = _sspecialries.Entity.GetById(model.Adm.SpecialtiesId);
            }
            catch
            {
                model.Specialties = null;
            }
        }

        public string UplodedFile(IFormFile Pic, string FolderName)
        {
            string FileName = null;
            if (Pic != null)
            {
                string UpLoadsFolder = Path.Combine(_hosting.WebRootPath, FolderName);
                FileName = Guid.NewGuid().ToString() + "_" + Pic.FileName;
                string FullPath = Path.Combine(UpLoadsFolder, FileName);

                using (var filestream = new FileStream(FullPath, FileMode.Create))
                {
                    Pic.CopyTo(filestream);
                }
            }
            return FileName;
        }

        public void DeleteFile(string FileName, string FolderName)
        {
            if (FileName != null)
            {
                string UploadesFolder = Path.Combine(_hosting.WebRootPath, FolderName);
                string FullPath = Path.Combine(UploadesFolder, FileName);
                System.IO.File.Delete(FullPath);
            }
        }


        public IndexAdminViewModel IndexView(IndexAdminViewModel Model)
        {
            try
            {
                Model.Admins = _aadmin.Entity.GetAll().Where(a => a.GategoryIdFk == 3).ToList();
            }
            catch
            {
                Model.Admins = null;
            }
            try
            {
                Model.Levels = _llevel.Entity.GetAll().ToList();
            }
            catch
            {
                Model.Levels = null;
            }
            try
            {
                Model.Specialties = _sspecialries.Entity.GetAll().ToList();
            }
            catch
            {
                Model.Specialties = null;
            }


            return Model;
        }

        public IndexAdminViewModel IndexView2(IndexAdminViewModel Model)
        {
            try
            {
                Model.Admins = _aadmin.Entity.GetAll().Where(a => a.GategoryIdFk == 2).ToList();
            }
            catch
            {
                Model.Admins = null;
            }
            try
            {
                Model.Levels = _llevel.Entity.GetAll().ToList();
            }
            catch
            {
                Model.Levels = null;
            }
            try
            {
                Model.Specialties = _sspecialries.Entity.GetAll().ToList();
            }
            catch
            {
                Model.Specialties = null;
            }


            return Model;
        }

        public AddAdminViewModel AddAdmin(AddAdminViewModel viewModel)
        {
            try
            {
                viewModel.Levels = _llevel.Entity.GetAll().ToList();
            }
            catch
            {
                viewModel.Levels = null;
            }
            try
            {
                viewModel.Specialties = _sspecialries.Entity.GetAll().ToList();
            }
            catch
            {
                viewModel.Specialties = null;
            }
            try
            {
                viewModel.Categores = _categores.Entity.GetAll().Where(c => c.GategoryId != 1).ToList();
            }
            catch
            {
                viewModel.Categores = null;
            }
         

            return viewModel;
        }

       

        public AddAdminViewModel EditAdmin(AddAdminViewModel viewModel , int id)
        {
            Admin ad = _aadmin.Entity.GetById(id);

            try
            {
                viewModel.Levels = _llevel.Entity.GetAll().Where(l => l.LevelId != -1).ToList();
            }
            catch
            {
                viewModel.Levels = null;
            }
            try
            {
                viewModel.Specialties = _sspecialries.Entity.GetAll().Where(s => s.SpecialtiesId != -1).ToList();
            }
            catch
            {
                viewModel.Specialties = null;
            }
            try
            {
                viewModel.Categores = _categores.Entity.GetAll().Where(c => c.GategoryId != -1 && c.GategoryId != 1).ToList();
            }
            catch
            {
                viewModel.Categores = null;
            }
            try
            {
                viewModel.LevId = ad.LevelIdFk;
            }
            catch
            {
                viewModel.LevId = 0;
            }
            try
            {
                viewModel.SpeId = ad.SpecialtiesId;
            }
            catch
            {
                viewModel.SpeId = 0;
            }
            try
            {
                viewModel.cateId = ad.GategoryIdFk;
            }
            catch
            {
                viewModel.cateId = 0;
            }
            try
            {
                viewModel.FullName = ad.FullName;
            }
            catch
            {
                viewModel.FullName = null;
            }
            try
            {
                viewModel.ImageUrl = ad.ImageUrl; 
            }
            catch
            {
                viewModel.ImageUrl = null;
            }

            try
            {
                viewModel.Password = ad.Password;
            }
            catch
            {
                viewModel.Password = null;
            }

            try
            {
                viewModel.UserName = ad.UserName; 
            }
            catch
            {
                viewModel.UserName = null;
            }

            try
            {
                viewModel.PhoneNumber = ad.PhoneNumper;
            }
            catch
            {
                viewModel.PhoneNumber = null;
            }

            try
            {
                viewModel.AdId = ad.AdminId;
            }
            catch
            {
                viewModel.AdId = 0;
            }

            return viewModel;
        }

    }
}
