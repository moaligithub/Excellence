using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing.Imaging;
using Thebes_Acadeny.Models;
using Thebes_Acadeny;
using Thebes_Acadeny.Models.Entity;

namespace myapp2.Controllers
{
    public class BooksController : Controller
    {
        private readonly IUnitOfWork<Plant> _plant;
        private readonly IUnitOfWork<Admin> _admin;
        private readonly IUnitOfWork<Books> _book;
        private readonly IHostingEnvironment _hosting;
        private readonly IUnitOfWork<Level> _level;
        private readonly IUnitOfWork<Specialties> _spe;
        private readonly IUnitOfWork<Tearm> _terme;
        private readonly IUnitOfWork<UrlWebSite> _url;

        public BooksController(IUnitOfWork<Plant> plant, IUnitOfWork<Admin> admin,
                                    IUnitOfWork<Books> book, IHostingEnvironment hosting,
                                    IUnitOfWork<Level> level, IUnitOfWork<Specialties> spe,
                                    IUnitOfWork<Tearm> terme, IUnitOfWork<UrlWebSite> url)
        {
            _plant = plant;
            _admin = admin;
            _book = book;
            _hosting = hosting;
            _level = level;
            _spe = spe;
            _terme = terme;
            _url = url;
        }
        // GET: BooksAdminController


        public ActionResult BooksOneAdmin(int id)
        {
            BooksStViewModel viewModel = new BooksStViewModel();

            try
            {
                viewModel.BooksAdmins = _book.Entity.GetAll().Where(b => b.AdminIdFk == id && b.Assent == true).OrderByDescending(b => b.BookId).ToList();
            }
            catch
            {
                viewModel.BooksAdmins = null;
            }
            try
            {
                viewModel.WebUrl = _url.Entity.GetAll().FirstOrDefault().UrlText;
            }
            catch
            {
                viewModel.WebUrl = null;
            }
            return View(viewModel);
        }

        public ActionResult Terme(int id)
        {
            ViewBag.adid = id;
            return View();
        }

        public ActionResult PlantIndex(int adid, int teid)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                PlantViewModel viewModel = new PlantViewModel();
                Admin admin = _admin.Entity.GetById(adid);
                Tearm terme = _terme.Entity.GetById(teid);
                try
                {
                    viewModel.Plants = _plant.Entity.GetAll().Where(p => p.LevelIdFk == admin.LevelIdFk && p.SpecialtiesIdFk == admin.SpecialtiesId && p.PlantId != -1 && p.Tearm == terme).ToList();
                }
                catch
                {
                    viewModel.Plants = null;
                }
                try
                {
                    viewModel.AdminId = adid;
                }
                catch
                {
                    viewModel.AdminId = 0;
                }
                
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult PlantIndex2(LevelIndexViewModel model)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                if (model.Teid != 0 && model.SpeId != -1 && model.LevId != -1)
                {
                    Tearm tearm = _terme.Entity.GetById(model.Teid);
                    PlantViewModel viewModel = new PlantViewModel();
                    try
                    {
                        viewModel.Plants = _plant.Entity.GetAll().Where(p => p.LevelIdFk == model.LevId && p.SpecialtiesIdFk == model.SpeId && p.Tearm == tearm && p.PlantId != -1).ToList();
                    }
                    catch
                    {
                        viewModel.Plants = null;
                    }
                    try
                    {
                        viewModel.AdminId = model.AdmId;
                    }
                    catch
                    {
                        viewModel.AdminId = 0;
                    }
                    return View(viewModel);
                }
                else
                {
                    if (model.LevId == -1)
                    {
                        ViewBag.lev = "من فضلك اختر المستوي";
                    }
                    if (model.SpeId == -1)
                    {
                        ViewBag.spe = "من فضلك اختر التخصص";
                    }
                    if (model.Teid == 0)
                    {
                        ViewBag.te = "من فضلك اختر التيرم";
                    }

                    LevelIndexViewModel model1 = new LevelIndexViewModel();
                    try
                    {
                        model1.Levels = _level.Entity.GetAll().ToList();
                    }
                    catch
                    {
                        model1.Levels = null;
                    }
                    try
                    {
                        model1.Specialties = _spe.Entity.GetAll().ToList();
                    }
                    catch
                    {
                        model1.Specialties = null;
                    }
                    try
                    {
                        model1.Tearms = _terme.Entity.GetAll().ToList();
                    }
                    catch
                    {
                        model1.Tearms = null;
                    }

                    return View("LevelIndex", model1);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LevelIndex(int id)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                LevelIndexViewModel viewModel = new LevelIndexViewModel()
                {
                    Levels = _level.Entity.GetAll().ToList(),
                    Specialties = _spe.Entity.GetAll().ToList(),
                    AdmId = id,
                    Tearms = _terme.Entity.GetAll().ToList()
                };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Index(int id, int IdAd)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                Plant plant = _plant.Entity.GetById(id);
                BooksStViewModel viewModel = new BooksStViewModel();
                try
                {
                    viewModel.BooksAdmins = _book.Entity.GetAll().Where(b => b.AdminIdFk == IdAd && b.Plant == plant).OrderByDescending(p => p.BookId).ToList();
                }
                catch
                {
                    viewModel.BooksAdmins = null;
                }
                try
                {
                    viewModel.PlantId = id;
                }
                catch
                {
                    viewModel.PlantId = 0;
                }
                try
                {
                    viewModel.AdminId = IdAd;
                }
                catch
                {
                    viewModel.AdminId = 0;
                }
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: BooksAdminController/Details/5
        //public ActionResult Details(int id)
        //{
        //    var book = _book.Entity.GetById(id);
        //    DetailsBookViewModel viewModel = new DetailsBookViewModel();
        //    try
        //    {
        //        viewModel.BooksAdmins = book;
        //    }
        //    catch
        //    {
        //        viewModel.BooksAdmins = null;
        //    }
        //    try
        //    {
        //        viewModel.Admin = _admin.Entity.GetById(book.AdminIdFk);
        //    }
        //    catch
        //    {
        //        viewModel.Admin = null;
        //    }

        //    return View(viewModel);
        //}


        // GET: BooksAdminController/Create
        public ActionResult Create(int id, int idad)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                Admin admin = _admin.Entity.GetById(idad);
                AddBookViewModel viewModel = new AddBookViewModel();
                try
                {
                    viewModel.Plants = _plant.Entity.GetAll().Where(p => p.SpecialtiesIdFk == admin.SpecialtiesId && p.LevelIdFk == admin.LevelIdFk).ToList();
                }
                catch
                {
                    viewModel.Plants = null;
                }
                try
                {
                    viewModel.pid = id;
                }
                catch
                {
                    viewModel.pid = 0;
                }
                try
                {
                    viewModel.AdminId = idad;
                }
                catch
                {
                    viewModel.AdminId = 0;
                }

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: BooksAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddBookViewModel viewModel, int idad, int id)
        {
            try
            {

                Books bookk = new Books
                {
                    BookName = viewModel.BookName,
                    AdminIdFk = viewModel.AdminId,
                    Plant = _plant.Entity.GetById(viewModel.pid),
                    PdfUrl = viewModel.NewPdfUrl
                };


                Admin admin = _admin.Entity.GetById(viewModel.AdminId);

                if (admin.GategoryIdFk == 1 || admin.GategoryIdFk == 2)
                {
                    bookk.Assent = true;
                }
                else
                {
                    bookk.Assent = false;
                }

                _book.Entity.Insert(bookk);
                _book.Save();

                Plant plant = _plant.Entity.GetById(id);
                BooksStViewModel odel = new BooksStViewModel();
                try
                {
                    odel.BooksAdmins = _book.Entity.GetAll().Where(b => b.AdminIdFk == idad && b.Plant == plant).OrderByDescending(p => p.BookId).ToList();
                }
                catch
                {
                    odel.BooksAdmins = null;
                }
                try
                {
                    odel.PlantId = id;
                }
                catch
                {
                    odel.PlantId = 0;
                }
                try
                {
                    odel.AdminId = idad;
                }
                catch
                {
                    odel.AdminId = 0;
                }

                return View("Index", odel);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Assent(int id, int plid)
        {
            Books book = _book.Entity.GetById(id);

            Books newbook = new Books()
            {
                AdminIdFk = book.AdminIdFk,
                Assent = true,
                BookName = book.BookName,
                PdfUrl = book.PdfUrl,
                Plant = _plant.Entity.GetById(plid)
            };

            _book.Entity.Insert(newbook);
            _book.Entity.Delete(id);
            _book.Save();
            return RedirectToAction("Order", "Admin");
        }

        // POST: BooksAdminController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int plid, int idad)
        {
            try
            {
                if (HttpContext.Session.GetInt32("IdAdmin") != null && id != -1)
                {
                    _book.Entity.Delete(id);
                    _book.Save();

                    Plant plant = _plant.Entity.GetById(plid);
                    BooksStViewModel odel = new BooksStViewModel();
                    try
                    {
                        odel.BooksAdmins = _book.Entity.GetAll().Where(b => b.AdminIdFk == idad && b.Plant == plant).OrderByDescending(p => p.BookId).ToList();
                    }
                    catch
                    {
                        odel.BooksAdmins = null;
                    }
                    try
                    {
                        odel.PlantId = plid;
                    }
                    catch
                    {
                        odel.PlantId = 0;
                    }
                    try
                    {
                        odel.AdminId = idad;
                    }
                    catch
                    {
                        odel.AdminId = 0;
                    }

                    return View("Index", odel);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("index", "Home");
            }
        }
    }
}
