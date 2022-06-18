using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Thebes_Acadeny;
using Thebes_Acadeny.Models;

namespace myapp2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Admin> _admin;
        private readonly IUnitOfWork<Specialties> _specialries;
        private readonly IUnitOfWork<Level> _level;
        private readonly IUnitOfWork<MessagesUser> _userMessage;
        private readonly IHostingEnvironment _hosting;

        public HomeController( IUnitOfWork<Admin> admin , IUnitOfWork<Specialties> specialries ,
                              IUnitOfWork<Level> level , IUnitOfWork<MessagesUser> UserMessage ,
                              IHostingEnvironment hosting)
        {
            _admin = admin;
            _specialries = specialries;
            _level = level;
            _userMessage = UserMessage;
            _hosting = hosting;
        }

        public ActionResult Index()
        {
            try
            {
                IndexViewModel model = new IndexViewModel()
                {
                    admins = _admin.Entity.GetAll().Where(a => a.GategoryIdFk == 3).ToList(),
                    adminShimaas = _admin.Entity.GetAll().Where(s =>  s.GategoryIdFk == 2).ToList(),
                    levels = _level.Entity.GetAll().Where(l => l.LevelId != -1).ToList(),
                    owner = _admin.Entity.GetAll().Where(o => o.GategoryIdFk == 1).SingleOrDefault(),
                    specialties = _specialries.Entity.GetAll().Where(s => s.SpecialtiesId != -1).ToList()
                };

                return View(model);
            }
            catch
            {
                return View();
            }
            
        }

        public ActionResult Levels (int id)
        {
            LevelViewModel model = new LevelViewModel();

            try
            {
                model.Levels = _level.Entity.GetAll().Where(l => l.LevelId != -1).ToList();
            }
            catch
            {
                model.Levels = null;
            }
            try
            {
                model.SpeId = id;
            }
            catch
            {
                model.SpeId = 0;
            }

            return View(model);
        }

        public ActionResult DetailsAdmin()
        {
            try
            {
                IndexViewModel indexView = new IndexViewModel()
                {
                    admins = _admin.Entity.GetAll().Where(a => a.GategoryIdFk == 3).ToList(),
                    adminShimaas = _admin.Entity.GetAll().Where(h => h.GategoryIdFk == 2).ToList(),
                    levels = _level.Entity.GetAll().Where(l => l.LevelId != -1).ToList(),
                    owner = _admin.Entity.GetAll().Where(o => o.GategoryIdFk == 1).FirstOrDefault(),
                    specialties = _specialries.Entity.GetAll().Where(s => s.SpecialtiesId != -1).ToList()
                };
                return View(indexView);

            }
            catch
            {
                return View();
            }
        }

        public ActionResult Message()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Message(MessagesUser model)
        {
            try
            {
                MessagesUser messagesUser = new MessagesUser()
                {
                    MessageText = model.MessageText,
                    DateTime = DateTime.Now,
                    Bol = false
                };
                _userMessage.Entity.Insert(messagesUser);
                _userMessage.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteMeesage(int id)
        { 
            _userMessage.Entity.Delete(id);
            _userMessage.Save();
       
            return RedirectToAction(nameof(DisplayMessage));
        }
        public ActionResult DisplayMessage()
        {
             DisplayMessage displayMessage = new DisplayMessage();
            try
            {
                displayMessage.messagesUsers = _userMessage.Entity.GetAll().OrderByDescending(m => m.MessageUserId).ToList();
            }
            catch
            {
                displayMessage.messagesUsers = null;
            }

            IList<MessagesUser> messa = _userMessage.Entity.GetAll().Where(m => m.Bol == false).ToList();

            foreach(var i in messa)
            {
                MessagesUser messages = new MessagesUser()
                {
                    MessageText = i.MessageText,
                    Bol = true,
                    DateTime = i.DateTime,
                };
                _userMessage.Entity.Insert(messages);
                _userMessage.Entity.Delete(i.MessageUserId);
            }
            _userMessage.Save();

            return View(displayMessage);
        }

        public ActionResult AddSpecialties()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSpecialties(SpeciltiesViewModel viewModel)
        { 
           try
           {  
                Specialties specialties = new Specialties()
                {
                    SpecialtiesName = viewModel.Name,
                };

                _specialries.Entity.Insert(specialties);
                _specialries.Save();

                return RedirectToAction(nameof(AllUpDateSpecialties));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AllUpDateSpecialties()
        {
            AllUpDateSpecialtiesViewModel allUpDate = new AllUpDateSpecialtiesViewModel()
            {
                Specialties = _specialries.Entity.GetAll().Where(s => s.SpecialtiesId != -1).ToList()
            };
            return View(allUpDate);   
        }
        
        
        public ActionResult UpDateSpecialties(int id)
        {      
            Specialties item = _specialries.Entity.GetById(id);
            SpeciltiesViewModel model = new SpeciltiesViewModel()
            {
                Name = item.SpecialtiesName,
                Id = item.SpecialtiesId
            };

            return View(model);
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpDateSpecialties(int id , SpeciltiesViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Specialties specialties = new Specialties()
                    {
                        SpecialtiesName = viewModel.Name,
                        SpecialtiesId = viewModel.Id
                    };

                    _specialries.Entity.UpDate(specialties);
                    _specialries.Save();
                }

                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

            }

            return RedirectToAction(nameof(AllUpDateSpecialties));
        }
    
        public ActionResult Terme(int LevId, int speid)
        {
            ViewBag.lev = LevId;
            ViewBag.spe = speid;
            return View();
        }
        public ActionResult DeleteSpecialties(int id)
        {
            if(id != 1)
            {
                _specialries.Entity.Delete(id);
                _specialries.Save();
                    
                return RedirectToAction(nameof(AllUpDateSpecialties));
            }
            else
            {
                return RedirectToAction(nameof(AllUpDateSpecialties));
            }  
        }

        //this method

        public string UplodedFile(IFormFile Pic, string FolderName)
        {
            string FileName = string.Empty;
            if(Pic != null)
            {
                string UpLoadsFolder = Path.Combine(_hosting.WebRootPath, FolderName);
                FileName = Guid.NewGuid().ToString() + "_" + Pic.FileName;
                string FullPath = Path.Combine(UpLoadsFolder, FileName);

                using(var filestream = new FileStream(FullPath , FileMode.Create))
                {
                    Pic.CopyTo(filestream);
                }
            }
            return FileName;
        }

        public void DeleteFile(string FileName , string FolderName)
        {
            if(FileName != null)
            {
                string UploadesFolder = Path.Combine(_hosting.WebRootPath, FolderName);
                string FullPath = Path.Combine(UploadesFolder, FileName);
                System.IO.File.Delete(FullPath);
            }
        }
        
    }
}
