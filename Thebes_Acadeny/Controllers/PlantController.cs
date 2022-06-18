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
    public class PlantController : Controller
    {
        private readonly IUnitOfWork<Plant> _plant;
        private readonly IUnitOfWork<Specialties> _spe;
        private readonly IUnitOfWork<Level> _level;
        private readonly IHostingEnvironment _hosting;
        private readonly IUnitOfWork<Tearm> _tearm;
        private readonly IUnitOfWork<Admin> _admin;

        public PlantController(IUnitOfWork<Plant> plant , IUnitOfWork<Specialties> spe , 
                               IUnitOfWork<Level> level , IHostingEnvironment hosting , 
                               IUnitOfWork<Tearm> tearm , IUnitOfWork<Admin> admin)
        {
            _plant = plant;
            _spe = spe;
            _level = level;
            _hosting = hosting;
            _tearm = tearm;
            _admin = admin;
        }                 
        // GET: PlantController
        public ActionResult Index(int levid , int teid , int speid)
        {
            Tearm tearm = _tearm.Entity.GetById(teid);
            PlantViewModel viewModel = new PlantViewModel();
            try
            {
                viewModel.Plants = _plant.Entity.GetAll().Where(p => p.LevelIdFk == levid && p.Tearm == tearm && p.SpecialtiesIdFk == speid && p.PlantId != -1).ToList();
            }
            catch
            {
                viewModel.Plants = null;
            }
            return View(viewModel);
        }

        // GET: PlantController/Details/5
        public ActionResult DetailsSpe()
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                IndexViewModel viewModel = new IndexViewModel();

                try
                {
                    viewModel.specialties = _spe.Entity.GetAll().Where(s => s.SpecialtiesId != -1).ToList();
                }
                catch
                {
                    viewModel.specialties = null;                     
                }

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }

        public ActionResult DetailsLev(int id)
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                LevelViewModel viewModel = new LevelViewModel();

                try
                {
                    viewModel.Levels = _level.Entity.GetAll().Where(l => l.LevelId != -1).ToList();
                }
                catch
                {
                    viewModel.Levels = null;
                }
                try
                {
                    viewModel.SpeId = id;
                }
                catch
                {
                    viewModel.SpeId = 0;
                }
                

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Terme(int speid , int levid)
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                ViewBag.speid = speid;
                ViewBag.levid = levid;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        public ActionResult DetailsPlant(int teid , int speid , int levid)
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                PlantViewModel viewModel = new PlantViewModel();
                Tearm terme = _tearm.Entity.GetById(teid);
                try
                {
                    viewModel.Plants = _plant.Entity.GetAll().Where(p => p.LevelIdFk == levid && p.Tearm == terme && p.SpecialtiesIdFk == speid && p.PlantId != -1).ToList();
                }
                catch
                {
                    viewModel.Plants = null;
                }
                ViewBag.teid = teid;
                ViewBag.speid = speid;
                ViewBag.levid = levid;
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("index", "Home");
            }
        }

        // GET: PlantController/Create
        public ActionResult Create(int teid , int levid , int speid)
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                ViewBag.teid = teid;
                ViewBag.levid = levid;
                ViewBag.speid = speid;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: PlantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddPlantViewModel viewModel)
        {
            try
            {
                Plant plant = new Plant
                {
                    PlantName = viewModel.Name,
                    LevelIdFk = viewModel.LevelId,
                    SpecialtiesIdFk = viewModel.speid,
                    Tearm = _tearm.Entity.GetById(viewModel.Tearm),
                };

                _plant.Entity.Insert(plant);
                _plant.Save();

                return RedirectToAction(nameof(DetailsSpe));

            }
            catch
            {
                return View();
            }
        }

        // GET: PlantController/Edit/5
        public ActionResult Edit(int id)
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                AddPlantViewModel viewModel = new AddPlantViewModel();
              
                try
                {
                    viewModel.Name = _plant.Entity.GetById(id).PlantName;
                }
                catch
                {
                    viewModel.Name = null;
                }
                try
                {
                    viewModel.Id = id;
                }
                catch
                {
                    viewModel.Id = 0;
                }
                try
                {
                    viewModel.Tearms = _tearm.Entity.GetAll().ToList();
                }
                catch
                {
                    viewModel.Tearms = null;
                }
                try
                {
                    viewModel.Tearm = _tearm.Entity.GetById(_plant.Entity.GetById(id).Tearm.Id).Id;
                }
                catch
                {
                    viewModel.Tearm = 0;
                }
                try
                {
                    viewModel.LevelId = _plant.Entity.GetById(id).LevelIdFk;
                }
                catch
                {
                    viewModel.LevelId = 0;
                }
                try
                {
                    viewModel.speid = _plant.Entity.GetById(id).SpecialtiesIdFk;
                }
                catch
                {
                    viewModel.speid = 0;
                }
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: PlantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddPlantViewModel viewModel)
        {
            try
            {
                Plant plant_1 = new Plant()
                {
                    PlantName = viewModel.Name,
                    PlantId = viewModel.Id,
                    LevelIdFk = viewModel.LevelId,
                    SpecialtiesIdFk = viewModel.speid,
                    Tearm = _tearm.Entity.GetById(viewModel.Tearm)
                };

                _plant.Entity.UpDate(plant_1);
                _plant.Save();
                
                
                return RedirectToAction(nameof(DetailsSpe));
            }
            catch
            {
                return RedirectToAction(nameof(DetailsSpe));
            }
        }

       

        // POST: PlantController/Delete/5
      
        public ActionResult Delete(int Id)
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                try
                {
                    if (Id != -1)
                    {
                        _plant.Entity.Delete(Id);
                        _plant.Save();
                    }

                    return RedirectToAction(nameof(DetailsSpe));
                }
                catch
                {
                    return RedirectToAction(nameof(DetailsPlant));
                }
            }
            else
            {
                return RedirectToAction("Index" , "Home");
            }
        }

        public ActionResult HomePlant(int id)
        {
            Plant plant = _plant.Entity.GetById(id);

            return View(plant);
        }

        public string UpLodedFile(IFormFile file , string path)
        {
            string filename = null;
            if(file != null)
            {
                string UpLoadsFolder = Path.Combine(_hosting.WebRootPath, path);
                filename = Guid.NewGuid().ToString() + "_" + file.FileName;
                string FullPath = Path.Combine(UpLoadsFolder, filename);

                using (var filestreame = new FileStream(FullPath, FileMode.Create))
                {
                    file.CopyTo(filestreame);
                }
            }
            return filename;
        }

        public void DeleteFile(string FileName , string path)
        {
            string UploadsFolder = Path.Combine(_hosting.WebRootPath, path);
            string FullPath = Path.Combine(UploadsFolder, FileName);
            System.IO.File.Delete(FullPath);
        }
    }
}
