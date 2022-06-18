using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny;
using Thebes_Acadeny.Models;
using Thebes_Acadeny.Models.Entity;

namespace myapp2.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork<Books> _books;
        private readonly IUnitOfWork<Plant> _plant;
        private readonly IUnitOfWork<Video> _video;
        private readonly IUnitOfWork<Posts> _post;
        private readonly IUnitOfWork<Admin> _admin;
        private readonly IUnitOfWork<Exam> _exam;
        private readonly IUnitOfWork<UrlWebSite> _url;

        public StudentController(IUnitOfWork<Books> books , IUnitOfWork<Plant> plant , 
                                 IUnitOfWork<Video> video , IUnitOfWork<Posts> post  , 
                                 IUnitOfWork<Admin> admin , IUnitOfWork<Exam> exam ,
                                 IUnitOfWork<UrlWebSite> url)
        {
            _books = books;
            _plant = plant;
            _video = video;
            _post = post;
            _admin = admin;
            _exam = exam;
            _url = url;
        }
        public IActionResult Pdf(int id)
        {
            BooksStViewModel viewModel = new BooksStViewModel();
            var item = _plant.Entity.GetById(id);
            try
            {
                viewModel.BooksAdmins = _books.Entity.GetAll().Where(a => a.Plant == item && a.Assent == true).OrderByDescending(a => a.BookId).ToList();
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
        public IActionResult Video(int id)
        {
            VideoStViewModel viewModel = new VideoStViewModel();
            var item = _plant.Entity.GetById(id);
            try
            {
                viewModel.VideoAdmins = _video.Entity.GetAll().Where(a => a.Plant == item && a.Assent == true).OrderByDescending(a => a.VideoId).ToList();
            }
            catch
            {
                viewModel.VideoAdmins = null;
            }
            

            return View(viewModel);
        }

        public ActionResult Posts(int id)
        {
            Plant pla = _plant.Entity.GetById(id);
            PostStViewModel viewModel = new PostStViewModel();

            try
            {
                viewModel.PostsAdmins = _post.Entity.GetAll().Where(p => p.Plant == pla && p.Assent == true).OrderByDescending(p => p.PostId).ToList();
            }
            catch
            {
                viewModel.PostsAdmins = null;
            }
            try
            {
                viewModel.Admins = _admin.Entity.GetAll().Where(a => a.LevelIdFk == pla.LevelIdFk && a.SpecialtiesId == pla.SpecialtiesIdFk).ToList();
            }
            catch
            {
                viewModel.Admins = null;
            }
            return View(viewModel);
        }
        public ActionResult Exam(int id)
        {
            Plant pla = _plant.Entity.GetById(id);

            ExamViewModel viewModel = new ExamViewModel();
            try
            {
                viewModel.Exams = _exam.Entity.GetAll().Where(e => e.Plant == pla && e.Assent == true).OrderByDescending(e => e.ExamId).ToList();
            }
            catch
            {
                viewModel.Exams = null;
            }
            try
            {
                viewModel.PlantId = id;
            }
            catch
            {
                viewModel.PlantId = 0;
            }

            return View(viewModel);
        }

    }
}
