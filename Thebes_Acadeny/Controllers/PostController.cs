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
    public class PostController : Controller
    {
        private readonly IUnitOfWork<Posts> _post;
        private readonly IUnitOfWork<Admin> _admin;
        private readonly IUnitOfWork<Plant> _plant;
        private readonly IHostingEnvironment _hosting;
        private readonly IUnitOfWork<Specialties> _spe;
        private readonly IUnitOfWork<Tearm> _tearm;
        private readonly IUnitOfWork<Level> _level;

        public PostController(IUnitOfWork<Posts> post , IUnitOfWork<Admin> admin , 
                              IUnitOfWork<Plant> plant , IHostingEnvironment hosting ,
                              IUnitOfWork<Level> level , IUnitOfWork<Specialties> spe ,
                              IUnitOfWork<Tearm> tearm)
        {
            _post = post;
            _admin = admin;
            _plant = plant;
            _hosting = hosting;
            _spe = spe;
            _tearm = tearm;
            _level = level;
        }
        public ActionResult PostsOneAdmin(int id)
        {
            PostStViewModel viewModel = new PostStViewModel();

            try
            {
                viewModel.PostsAdmins = _post.Entity.GetAll().Where(p => p.AdminIdFk == id && p.Assent == true).OrderByDescending(p => p.PostId).ToList();
            }
            catch
            {
                viewModel.PostsAdmins = null;
            }
            try
            {
                viewModel.Admin = _admin.Entity.GetById(id);
            }
            catch
            {
                viewModel.Admin = null;
            }
            

            return View(viewModel);
        }


        public ActionResult PlantIndex(int adid , int teid)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                Tearm tearm = _tearm.Entity.GetById(teid);
                PlantViewModel viewModel = new PlantViewModel();
                Admin admin = _admin.Entity.GetById(adid);
                try
                {
                    viewModel.Plants = _plant.Entity.GetAll().Where(p => p.LevelIdFk == admin.LevelIdFk && p.Tearm == tearm && p.SpecialtiesIdFk == admin.SpecialtiesId && p.PlantId != -1).ToList();
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

        public ActionResult Details(int id)
        {
            Posts posts = _post.Entity.GetById(id);

            return View(posts);
        }

        public ActionResult Assent(int id , int plid)
        {
            Posts post = _post.Entity.GetById(id);

            Posts newpost = new Posts()
            {
                AdminIdFk = post.AdminIdFk,
                Assent = true,
                ImageUrl = post.ImageUrl,
                Text = post.Text,
                Plant = _plant.Entity.GetById(plid)
            };

            _post.Entity.Insert(newpost);
            _post.Entity.Delete(id);
            _post.Save();
            return RedirectToAction("Order", "Admin");
        }
        // GET: BookOwnerController
        public ActionResult Index(int id , int idad)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                Plant plant = _plant.Entity.GetById(id);
                PostStViewModel viewModel = new PostStViewModel();
                try
                {
                    viewModel.PostsAdmins = _post.Entity.GetAll().Where(b => b.AdminIdFk == idad && b.Plant == plant).OrderByDescending(p => p.PostId).ToList();
                }
                catch
                {
                    viewModel.PostsAdmins = null;
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
                    viewModel.Admin = _admin.Entity.GetById(idad);
                }
                catch
                {
                    viewModel.Admin = null;
                }
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: BookOwnerController/Details/5
        //public ActionResult Details(int id)
        //{
        //    var book = _book.Entity.GetById(id);
        //    DetailsBookOwnerViewModel viewModel = new DetailsBookOwnerViewModel();
        //    try
        //    {
        //        viewModel.BooksOwner = book;
        //    }
        //    catch
        //    {
        //        viewModel.BooksOwner = null;
        //    }
        //    try
        //    {
        //        viewModel.Owner = _owner.Entity.GetById(book.OwnerIdFk);
        //    }
        //    catch
        //    {
        //        viewModel.Owner = null;
        //    }

        //    return View(viewModel);
        //}

        // GET: BookOwnerController/Create
        public ActionResult Create(int id , int idad)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                var ownerr = _admin.Entity.GetById(idad);
                AddPostViewModel viewModel = new AddPostViewModel();
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

        // POST: BookOwnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddPostViewModel viewModel , int idad , int plid)
        {
            try
            {
                string FillName = UplodedFile(viewModel.File, @"Tmb1/Posts");
                Posts Post = new Posts
                {
                    Text = viewModel.Text,
                    AdminIdFk = viewModel.AdminId, 
                    Plant = _plant.Entity.GetById(viewModel.pid),
                };

                Admin admin = _admin.Entity.GetById(viewModel.AdminId);

                if(admin.GategoryIdFk == 1 || admin.GategoryIdFk == 2)
                {
                    Post.Assent = true;
                }
                else
                {
                    Post.Assent = false;
                }

                if (FillName != null)
                {
                    Post.ImageUrl = FillName;
                }
                else
                {
                    Post.ImageUrl = null;
                }


                _post.Entity.Insert(Post);
                _post.Save();

                Plant plant = _plant.Entity.GetById(plid);
                PostStViewModel odel = new PostStViewModel();
                try
                {
                    odel.PostsAdmins = _post.Entity.GetAll().Where(b => b.AdminIdFk == idad && b.Plant == plant).OrderByDescending(p => p.PostId).ToList();
                }
                catch
                {
                    odel.PostsAdmins = null;
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
                    odel.Admin = _admin.Entity.GetById(idad);
                }
                catch
                {
                    odel.Admin = null;
                }
                return View("Index" , odel);
            }
            catch
            {
                return View();
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
                    Tearms = _tearm.Entity.GetAll().ToList()
                };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        // GET: BookOwnerController/Edit/5
        public ActionResult Edit(int id , int idad , int plid)
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                Posts post = _post.Entity.GetById(id);
                AddPostViewModel viewModel = new AddPostViewModel
                {
                    Text = post.Text,
                    id = id,
                    ImageUrl = post.ImageUrl,
                    AdminId = post.AdminIdFk,
                    pid = plid
                };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        // POST: BookOwnerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddPostViewModel viewModel)
        {
            try
            {
                Posts postsOwner = new Posts
                {
                    Text = viewModel.Text,
                    PostId = viewModel.id,
                    AdminIdFk = viewModel.AdminId
                };

                if(viewModel.File != null)
                {
                    postsOwner.ImageUrl = UplodedFile(viewModel.File, @"Tmb1/Posts");
                    DeleteFile(viewModel.ImageUrl, @"Tmb1/Posts");
                }
                else
                {
                    postsOwner.ImageUrl = viewModel.ImageUrl;
                }

                _post.Entity.UpDate(postsOwner);
                _post.Save();

                Plant plant = _plant.Entity.GetById(viewModel.pid);
                PostStViewModel odel = new PostStViewModel();
                try
                {
                    odel.PostsAdmins = _post.Entity.GetAll().Where(b => b.AdminIdFk == viewModel.AdminId && b.Plant == plant).OrderByDescending(p => p.PostId).ToList();
                }
                catch
                {
                    odel.PostsAdmins = null;
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
                    odel.Admin = _admin.Entity.GetById(viewModel.AdminId);
                }
                catch
                {
                    odel.Admin = null;
                }
                return View("Index", odel);
            }
            catch
            {
                return View();
            }
        }

        
        // POST: BookOwnerController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id , int idad, int plid)
        {
            try
            {
                if (HttpContext.Session.GetInt32("IdAdmin") != null && id != -1)
                {
                    DeleteFile(_post.Entity.GetById(id).ImageUrl, @"Tmb1/Posts");
                    _post.Entity.Delete(id);
                    _post.Save();

                    Plant plant = _plant.Entity.GetById(plid);
                    PostStViewModel odel = new PostStViewModel();
                    try
                    {
                        odel.PostsAdmins = _post.Entity.GetAll().Where(b => b.AdminIdFk == idad && b.Plant == plant).OrderByDescending(p => p.PostId).ToList();
                    }
                    catch
                    {
                        odel.PostsAdmins = null;
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
                        odel.Admin = _admin.Entity.GetById(idad);
                    }
                    catch
                    {
                        odel.Admin = null;
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
    }
}
