 using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny;
using Thebes_Acadeny.Models;
using Thebes_Acadeny.Models.Entity;

namespace myapp2.Controllers
{
    public class VideoController : Controller
    {
        private readonly IUnitOfWork<Video> _video;
        private readonly IUnitOfWork<Admin> _admin;
        private readonly IUnitOfWork<Plant> _plant;
        private readonly IHostingEnvironment _hosting;
        private readonly IUnitOfWork<Level> _level;
        private readonly IUnitOfWork<Specialties> _spe;
        private readonly IUnitOfWork<Tearm> _tearm;

        public VideoController(IUnitOfWork<Video> video , IUnitOfWork<Admin> admin , 
                               IUnitOfWork<Plant> plant , IHostingEnvironment hosting , 
                               IUnitOfWork<Level> level , IUnitOfWork<Specialties> spe ,
                               IUnitOfWork<Tearm> tearm)
        {
            _video = video;
            _admin = admin;
            _plant = plant;
            _hosting = hosting;
            _level = level;
            _spe = spe;
            _tearm = tearm;
        }
        public ActionResult VideoOneAdmin(int id)
        {
            VideoStViewModel viewModel = new VideoStViewModel();
            try
            {
                viewModel.VideoAdmins = _video.Entity.GetAll().Where(a => a.AdminIdFk == id && a.Assent == true).OrderByDescending(a => a.VideoId).ToList();
            }
            catch
            {
                viewModel.VideoAdmins = null;
            }

            return View(viewModel);
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
                    Tearms = _tearm.Entity.GetAll().ToList()
                };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Terme(int id)
        {
            ViewBag.adid = id;
            return View();
        }

        public ActionResult PlantIndex2(LevelIndexViewModel model)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                if (model.Teid != 0 && model.SpeId != -1 && model.LevId != -1)
                {
                    Tearm tearm = _tearm.Entity.GetById(model.Teid);
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
                        model1.Tearms = _tearm.Entity.GetAll().ToList();
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

        public ActionResult PlantIndex(int adid , int teid)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                PlantViewModel viewModel = new PlantViewModel();
                Admin admin = _admin.Entity.GetById(adid);
                Tearm tearm = _tearm.Entity.GetById(teid);
                try
                {
                    viewModel.Plants = _plant.Entity.GetAll().Where(p => p.LevelIdFk == admin.LevelIdFk && p.SpecialtiesIdFk == admin.SpecialtiesId && p.Tearm == tearm && p.PlantId != -1).ToList();
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

        // GET: BookOwnerController
        public ActionResult Index(int id , int adid)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                Plant plant = _plant.Entity.GetById(id);
                VideoStViewModel viewModel = new VideoStViewModel();
                try
                {
                    viewModel.VideoAdmins = _video.Entity.GetAll().Where(b => b.AdminIdFk == adid && b.Plant == plant).ToList();
                }
                catch
                {
                    viewModel.VideoAdmins = null;
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

        // GET: BookOwnerController/Details/5
        public ActionResult Details(int id)
        {
            var Video = _video.Entity.GetById(id);
            DetailsVideoViewModel viewModel = new DetailsVideoViewModel();
            try
            {
                viewModel.VideoAdmin = Video;
            }
            catch
            {
                viewModel.VideoAdmin = null;
            }
            try
            {
                viewModel.Admin = _admin.Entity.GetById(Video.AdminIdFk);
            }
            catch
            {
                viewModel.Admin = null;
            }

            return View(viewModel);
        }

        // GET: BookOwnerController/Create
        public ActionResult Create(int id , int Adid)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                var admin = _admin.Entity.GetById(Adid);
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
                    viewModel.AdminId = Adid;
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

        // POST: BookOwnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddBookViewModel viewModel)
        {
            try
            {
                Video video = new Video
                {
                    VideoUrl = viewModel.NewPdfUrl,
                    Text = viewModel.BookName,
                    AdminIdFk = viewModel.AdminId,
                    Plant = _plant.Entity.GetById(viewModel.pid)
                };

                Admin admin = _admin.Entity.GetById(viewModel.AdminId);

                if (admin.GategoryIdFk == 1 || admin.GategoryIdFk == 2)
                {
                    video.Assent = true;
                }
                else
                {
                    video.Assent = false;
                }

                _video.Entity.Insert(video);
                _video.Save();


                Plant plant = _plant.Entity.GetById(viewModel.pid);
                VideoStViewModel odel = new VideoStViewModel();
                try
                {
                    odel.VideoAdmins = _video.Entity.GetAll().Where(b => b.AdminIdFk == viewModel.AdminId && b.Plant == plant).ToList();
                }
                catch
                {
                    odel.VideoAdmins = null;
                }
                try
                {
                    odel.PlantId = viewModel.pid;
                }
                catch
                {
                    odel.PlantId = 0;
                }
                try
                {
                    odel.AdminId = viewModel.AdminId;
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

        //// GET: BookOwnerController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: BookOwnerController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken] 
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: BookOwnerController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: BookOwnerController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id , int plid , int adid)
        {
            try
            {
                if (HttpContext.Session.GetInt32("IdAdmin") != null && id != -1)
                {
                    _video.Entity.Delete(id);
                    _video.Save();
                    
                    Plant plant = _plant.Entity.GetById(plid);
                    VideoStViewModel odel = new VideoStViewModel();
                    try
                    {
                        odel.VideoAdmins = _video.Entity.GetAll().Where(b => b.AdminIdFk == adid && b.Plant == plant).ToList();
                    }
                    catch
                    {
                        odel.VideoAdmins = null;
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
                        odel.AdminId = adid;
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
                return RedirectToAction("Index" , "Home");
            }
        }

        public ActionResult Assent(int id , int plid)
        {
            Video video = _video.Entity.GetById(id);

            Video newvideo = new Video()
            {
                AdminIdFk = video.AdminIdFk,
                Assent = true,
                Text = video.Text,
                VideoUrl = video.VideoUrl,
                Plant = _plant.Entity.GetById(plid)
            };

            _video.Entity.Insert(newvideo);
            _video.Entity.Delete(id);
            _video.Save();

            return RedirectToAction("Order", "Admin");
        }
    }
}
