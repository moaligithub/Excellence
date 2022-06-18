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
using Thebes_Acadeny.ViewModel;

namespace myapp2.Controllers
{
    public class ExamController : Controller
    {
        private readonly IUnitOfWork<Exam> _exam;
        private readonly IUnitOfWork<Plant> _plant;
        private readonly IUnitOfWork<Admin> _admin;
        private readonly IUnitOfWork<Question> _question;
        private readonly IUnitOfWork<Answer> _answer;
        private readonly IUnitOfWork<Question_True_or_false> _questiontrueorfalse;
        private readonly IUnitOfWork<Level> _level;
        private readonly IUnitOfWork<Specialties> _spe;
        private readonly IUnitOfWork<Tearm> _tearm;
        private readonly IHostingEnvironment _hosting;

        public ExamController(IUnitOfWork<Exam> exam, IUnitOfWork<Plant> plant,
                              IUnitOfWork<Admin> admin, IUnitOfWork<Question> question,
                              IUnitOfWork<Answer> answer , IUnitOfWork<Question_True_or_false> questiontrueorfalse , 
                              IUnitOfWork<Level> level , IUnitOfWork<Specialties> spe ,
                              IUnitOfWork<Tearm> tearm , IHostingEnvironment hosting)
        {
            _exam = exam;
            _plant = plant;
            _admin = admin;
            _question = question;
            _answer = answer;
            _questiontrueorfalse = questiontrueorfalse;
            _level = level;
            _spe = spe;
            _tearm = tearm;
            _hosting = hosting;
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
            Admin admin = _admin.Entity.GetById(adid);
            Tearm tearm = _tearm.Entity.GetById(teid);
            PlantViewModel pla = new PlantViewModel();
            try
            {
                pla.Plants = _plant.Entity.GetAll().Where(p => p.LevelIdFk == admin.LevelIdFk && p.SpecialtiesIdFk == admin.SpecialtiesId && p.Tearm == tearm).ToList();
            }
            catch
            {
                pla.Plants = null;
            }
            try
            {
                pla.AdminId = adid;
            }
            catch
            {
                pla.AdminId = 0;
            }

            return View(pla);
        }

        public ActionResult Terme(int id)
        {
            ViewBag.adid = id;
            return View();
        }

        // GET: ExamAdminController
        public ActionResult Index(int id, int idad)
        {
            Plant pla = _plant.Entity.GetById(id);

            ExamIndexViewModel exam = new ExamIndexViewModel();

            try
            {
                exam.Exams = _exam.Entity.GetAll().Where(e => e.AdminIdFk == idad && e.Plant == pla).ToList();
            }
            catch
            {
                exam.Exams = null;
            }
            try
            {
                exam.IdAdmin = idad;
            }
            catch
            {
                exam.IdAdmin = 0;
            }
            try
            {
                exam.PlantId = id;
            }
            catch
            {
                exam.PlantId = 0;
            }
            return View(exam);
        }

        public ActionResult ExamOneAdmin(int id)
        {
            Admin admin = _admin.Entity.GetById(id);
            ExamViewModel viewModel = new ExamViewModel();
            try
            {
                viewModel.Exams = _exam.Entity.GetAll().Where(e => e.AdminIdFk == id && e.Assent == true).OrderByDescending(e => e.ExamId).ToList();
            }
            catch
            {
                viewModel.Exams = null;
            }
            try
            {
                viewModel.Plants = _plant.Entity.GetAll().Where(p => p.LevelIdFk == admin.LevelIdFk && p.SpecialtiesIdFk == admin.SpecialtiesId).ToList();
            }
            catch
            {
                viewModel.Plants = null;
            }
            return View(viewModel);
        }


        // GET: ExamAdminController/Create
        public ActionResult Create1(int id, int idpl)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                AddExamViewModel_1 ad = new AddExamViewModel_1();
                try
                {
                    ad.PlantId = idpl;
                }
                catch
                {
                    ad.PlantId = 0;
                }
                try
                {
                    ad.AdminId = id;
                }
                catch
                {
                    ad.AdminId = 0;
                }
                return View(ad);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Create2(AddExamViewModel_1 viewModel_1)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                AddExamViewModel_1 model = new AddExamViewModel_1
                {
                    ExamName = viewModel_1.ExamName,
                    AdminId = viewModel_1.AdminId,
                    PlantId = viewModel_1.PlantId,
                    ExamId = viewModel_1.ExamId
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Create3(AddExamViewModel_1 viewModel_1)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                AddExamViewModel_1 model = new AddExamViewModel_1
                {
                    ExamName = viewModel_1.ExamName,
                    AdminId = viewModel_1.AdminId,
                    PlantId = viewModel_1.PlantId,
                    ExamId = viewModel_1.ExamId
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Cre1(AddExamViewModel_1 viewModel_1)
        {
            try
            {
                Exam exam = new Exam
                {
                    Title = viewModel_1.ExamName,
                    AdminIdFk = viewModel_1.AdminId,
                    Plant = _plant.Entity.GetById(viewModel_1.PlantId)
                };

                Admin admin = _admin.Entity.GetById(viewModel_1.AdminId);

                if (admin.GategoryIdFk == 1 || admin.GategoryIdFk == 2)
                {
                    exam.Assent = true;
                }
                else
                {
                    exam.Assent = false;
                }

                _exam.Entity.Insert(exam);
                _exam.Save();

                AddExamViewModel_1 model = new AddExamViewModel_1
                {
                    ExamName = viewModel_1.ExamName,
                    AdminId = viewModel_1.AdminId,
                    PlantId = viewModel_1.PlantId,
                    ExamId = exam.ExamId
                };
                return View("TypeQuestion", model);
            }
            catch 
            {
                return RedirectToAction("Index" , "Home");
            }
        }
        public ActionResult Cre2(AddExamViewModel_1 viewModel_1)
        {
            try
            {
                if(viewModel_1.Qusetion != null)
                {
                    Question question = new Question
                    {
                        Title = viewModel_1.Qusetion,
                        ExamIdFk = viewModel_1.ExamId,
                    };
                    if(viewModel_1.file != null)
                    {
                        question.img = UplodedFile(viewModel_1.file , @"Tmb1/img/ExamsImages");
                    }
                    _question.Entity.Insert(question);
                    _question.Save();

                    foreach (var item in viewModel_1.Answer)
                    {
                        Answer answer = new Answer
                        {
                            AnswerTitle = item,
                            boolAnswer = false,
                            QuestionId = question.QuestionId,
                            Exam = _exam.Entity.GetById(viewModel_1.ExamId)
                        };
                        _answer.Entity.Insert(answer);
                    }
                    _answer.Save();
                }
                else
                {
                    Question_True_or_false newquestion = new Question_True_or_false
                    {
                        Title = viewModel_1.QusetionTrueOrFalse,
                        ExamIdFk = viewModel_1.ExamId
                    };
                    if(viewModel_1.AnswerTrueOrFalse == 1)
                    {
                        newquestion.Answer = true;
                    }
                    else if(viewModel_1.AnswerTrueOrFalse == 2)
                    {
                        newquestion.Answer = false;
                    }
                    if(viewModel_1.file != null)
                    {
                        newquestion.img = UplodedFile(viewModel_1.file, @"Tmb1/img/ExamsImages");
                    }
                    _questiontrueorfalse.Entity.Insert(newquestion);
                    _questiontrueorfalse.Save();
                }


                AddExamViewModel_1 model = new AddExamViewModel_1
                {
                    ExamName = viewModel_1.ExamName,
                    AdminId = viewModel_1.AdminId,
                    PlantId = viewModel_1.PlantId,
                    ExamId = viewModel_1.ExamId
                };
                return View("TypeQuestion", model);
            }
            catch
            {
                _exam.Entity.Delete(viewModel_1.ExamId);
                _exam.Save();
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult TypeQuestion(AddExamViewModel_1 viewModel_1)
        {
            AddExamViewModel_1 model = new AddExamViewModel_1
            {
                ExamId = viewModel_1.ExamId,
                AdminId = viewModel_1.AdminId,
                PlantId = viewModel_1.PlantId,
                ExamName = viewModel_1.ExamName
            };
            return View(model);
        }
        public ActionResult Cre3(AddExamViewModel_1 viewModel_1)
        {
            try
            {
                if (viewModel_1.Qusetion != null)
                {
                    Question question = new Question
                    {
                        Title = viewModel_1.Qusetion,
                        ExamIdFk = viewModel_1.ExamId,
                    };
                    
                    if (viewModel_1.file != null)
                    {
                        question.img = UplodedFile(viewModel_1.file, @"Tmb1/img/ExamsImages");
                    }
                    _question.Entity.Insert(question);
                    _question.Save();

                    foreach (var item in viewModel_1.Answer)
                    {
                        Answer answer = new Answer
                        {
                            AnswerTitle = item,
                            boolAnswer = false,
                            QuestionId = question.QuestionId,
                            Exam = _exam.Entity.GetById(viewModel_1.ExamId)
                        };
                        _answer.Entity.Insert(answer);
                    }
                    _answer.Save();
                }
                else
                {
                    Question_True_or_false newquestion = new Question_True_or_false
                    {
                        Title = viewModel_1.QusetionTrueOrFalse,
                        ExamIdFk = viewModel_1.ExamId
                    };
                    if (viewModel_1.AnswerTrueOrFalse == 1)
                    {
                        newquestion.Answer = true;
                    }
                    else if (viewModel_1.AnswerTrueOrFalse == 2)
                    {
                        newquestion.Answer = false;
                    }

                    if (viewModel_1.file != null)
                    {
                        newquestion.img = UplodedFile(viewModel_1.file, @"Tmb1/img/ExamsImages");
                    }

                    _questiontrueorfalse.Entity.Insert(newquestion);
                    _questiontrueorfalse.Save();
                }

                var items = _question.Entity.GetAll().Where(q => q.ExamIdFk == viewModel_1.ExamId).ToList();

                if(items.Count != 0)
                {
                    CreateIndexExamViewModel index = new CreateIndexExamViewModel();

                    index.Examm = _exam.Entity.GetById(viewModel_1.ExamId);
                    index.Questionss = _question.Entity.GetAll().Where(q => q.ExamIdFk == viewModel_1.ExamId).ToList();
                    index.Answerss = _answer.Entity.GetAll().Where(a => a.Exam == index.Examm).ToList();

                    ViewBag.AdminId = viewModel_1.AdminId;
                    ViewBag.PlantId = viewModel_1.PlantId;

                    return View("CreateIndex", index);
                }
                else
                {
                    Plant pla = _plant.Entity.GetById(viewModel_1.PlantId);

                    ExamIndexViewModel exam = new ExamIndexViewModel();

                    try
                    {
                        exam.Exams = _exam.Entity.GetAll().Where(e => e.AdminIdFk == viewModel_1.AdminId && e.Plant == pla).ToList();
                    }
                    catch
                    {
                        exam.Exams = null;
                    }
                    try
                    {
                        exam.IdAdmin = viewModel_1.AdminId;
                    }
                    catch
                    {
                        exam.IdAdmin = 0;
                    }
                    try
                    {
                        exam.PlantId = viewModel_1.PlantId;
                    }
                    catch
                    { 
                        exam.PlantId = 0;
                    }
                    return View("Index", exam);
                }
            }
            catch
            {
                _exam.Entity.Delete(viewModel_1.ExamId);
                _exam.Save();
                return RedirectToAction("Index" , "Home");
            }
        }   
        
        public ActionResult Edit(int id)
        {
            Exam exam = _exam.Entity.GetById(id);
            var question = _question.Entity.GetAll().Where(q => q.ExamIdFk == id).ToList();
            var answer = _answer.Entity.GetAll().Where(a => a.Exam == exam).ToList();
            var question_true_false = _questiontrueorfalse.Entity.GetAll().Where(q => q.ExamIdFk == id).ToList();
            DetailsExamViewModel viewModel = new DetailsExamViewModel();

            try
            {
                viewModel.Exam = exam;
            }
            catch
            {
                viewModel.Exam = null;
            }
            try
            {
                viewModel.Questions = question;
            }
            catch
            {
                viewModel.Questions = null;
            }
            try
            {
                viewModel.Question_True_Or_Falses = question_true_false;
            }
            catch
            {
                viewModel.Question_True_Or_Falses = null;
            }
            try
            {
                viewModel.Answers = answer;
            }
            catch
            {
                viewModel.Answers = null;
            }

            return View(viewModel);
        }

        public ActionResult Edit2(int id)
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                var qu = _question.Entity.GetById(id);
                EditQuestionViewModel viewModel = new EditQuestionViewModel();
                try
                {
                    viewModel.ExamId = qu.ExamIdFk;
                }
                catch
                {
                    viewModel.ExamId = 0;
                }
                try
                {
                    viewModel.QusetionName = qu.Title;
                }
                catch
                {
                    viewModel.QusetionName = null;
                }
                try
                {
                    viewModel.Answer = _answer.Entity.GetAll().Where(a => a.QuestionId == id).Select(a => a.AnswerTitle).ToList();
                }
                catch
                {
                    viewModel.Answer = null;
                }

                if (qu.img != null)
                {
                    viewModel.imgurl = qu.img;
                }
                else
                {
                    viewModel.imgurl = null;
                }
                try
                {
                    viewModel.QuestionId = qu.QuestionId;
                }
                catch
                {
                    viewModel.QuestionId = 0;
                }
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2(EditQuestionViewModel viewModel)
        {
            Question question = new Question
            {
                Title = viewModel.QusetionName,
                ExamIdFk = viewModel.ExamId,
                QuestionId = viewModel.QuestionId
            };
            if(viewModel.file != null)
            {
                question.img = UplodedFile(viewModel.file, @"Tmb1/img/ExamsImages");
                DeleteFile(viewModel.imgurl, @"Tmb1/img/ExamsImages");
            }
            else
            {
                question.img = viewModel.imgurl;
            }
            _question.Entity.UpDate(question);
            _question.Save();

            var Answers = _answer.Entity.GetAll().Where(a => a.QuestionId == viewModel.QuestionId).ToList();
            int Count = 0;
            var q = _question.Entity.GetById(viewModel.QuestionId);
            foreach(var item in viewModel.Answer)
            {
                Answer an = Answers[Count];

                an.AnswerTitle = item;
                an.boolAnswer = false;

                _answer.Entity.UpDate(an);
                Count++;
            }
            _answer.Save();



            var ex = _exam.Entity.GetById(viewModel.ExamId);

            Admin admin = _admin.Entity.GetById(HttpContext.Session.GetInt32("IdAdmin").Value);

            if (admin.GategoryIdFk == 1 || admin.GategoryIdFk == 2)
            {
                ex.Assent = true;
            }
            else
            {
                ex.Assent = false;
            }

            _exam.Entity.UpDate(ex);
            _exam.Save();

            CreateIndexExamViewModel mmodel = new CreateIndexExamViewModel
            {
                ExamId = viewModel.ExamId,
                Answerss = _answer.Entity.GetAll().Where(a => a.QuestionId == viewModel.QuestionId).ToList(),
                QuestionName = _question.Entity.GetById(viewModel.QuestionId).Title
            };

            return View("EditTrue" , mmodel);
        }

        public ActionResult Edit3(int id)
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                var qu = _questiontrueorfalse.Entity.GetById(id);
                EditQuestionViewModel viewModel = new EditQuestionViewModel();
                try
                {
                    viewModel.ExamId = qu.ExamIdFk;
                }
                catch
                {
                    viewModel.ExamId = 0;
                }
                try
                {
                    viewModel.question_True_Or_False_Name = qu.Title;
                }
                catch
                {
                    viewModel.question_True_Or_False_Name = null;
                }

                if (qu.img != null)
                {
                    viewModel.imgurl = qu.img;
                }
                else
                {
                    viewModel.imgurl = null;
                }
                try
                {
                    viewModel.question_true_false_Id = qu.Id;
                }
                catch
                {
                    viewModel.question_true_false_Id = 0;
                }
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit3(EditQuestionViewModel viewModel)
        {
            var question = _questiontrueorfalse.Entity.GetById(viewModel.question_true_false_Id);
            question.Title = viewModel.question_True_Or_False_Name;
            if(viewModel.file != null)
            {
                question.img = UplodedFile(viewModel.file, @"Tmb1/img/ExamsImages");
                DeleteFile(viewModel.imgurl, @"Tmb1/img/ExamsImages");
            }
            if (viewModel.AnswerTrue == 1)
            {
                question.Answer = true;
            }
            else if(viewModel.AnswerTrue == 2)
            {
                question.Answer = false;
            }
            _questiontrueorfalse.Entity.UpDate(question);
            _questiontrueorfalse.Save();

            var ex = _exam.Entity.GetById(viewModel.ExamId);

            Admin admin = _admin.Entity.GetById(HttpContext.Session.GetInt32("IdAdmin").Value);

            if (admin.GategoryIdFk == 1 || admin.GategoryIdFk == 2)
            {
                ex.Assent = true;
            }
            else
            {
                ex.Assent = false;
            }

            _exam.Entity.UpDate(ex);
            _exam.Save();

            return RedirectToAction("Edit", new { id = viewModel.ExamId });
        }
        public ActionResult Delete(int id , int idad, int plid)
        {
            try
            {
                if (HttpContext.Session.GetInt32("IdAdmin") != null)
                {
                    var img1 = _question.Entity.GetAll().Where(q => q.ExamIdFk == id && q.img != null);
                    if(img1 != null)
                    {
                        foreach (var item in img1)
                        {
                            DeleteFile(item.img, @"Tmb1/img/ExamsImages");
                        }
                    }

                    var img2 = _questiontrueorfalse.Entity.GetAll().Where(q => q.ExamIdFk == id && q.img != null).ToList();
                    if(img2 != null)
                    {
                        foreach (var item2 in img2)
                        {
                            DeleteFile(item2.img, @"Tmb1/img/ExamsImages");
                        }
                    }

                    _exam.Entity.Delete(id);
                    _exam.Save();

                    Plant pla = _plant.Entity.GetById(plid);

                    ExamIndexViewModel exam = new ExamIndexViewModel();

                    try 
                    {
                        exam.Exams = _exam.Entity.GetAll().Where(e => e.AdminIdFk == idad && e.Plant == pla).ToList();
                    }
                    catch
                    {
                        exam.Exams = null;
                    }
                    try
                    {
                        exam.IdAdmin = idad;
                    }
                    catch
                    {
                        exam.IdAdmin = 0;
                    }
                    try
                    {
                        exam.PlantId = plid;
                    }
                    catch
                    {
                        exam.PlantId = 0;
                    }
                    return View("Index" , exam);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction(nameof(PlantIndex));
            }
        }

        public ActionResult Delete2(int id , int exid)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                var question = _question.Entity.GetById(id);

                if (question.img != null)
                {
                    DeleteFile(question.img, @"Tmb1/img/ExamsImages");
                }

                _question.Entity.Delete(id);
                _question.Save();

                return RedirectToAction("Edit", new { id = exid });

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Delete3(int id , int exid)
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                var question = _questiontrueorfalse.Entity.GetById(id);
                if (question.img != null)
                {
                    DeleteFile(question.img, @"Tmb1/img/ExamsImages");
                }

                _questiontrueorfalse.Entity.Delete(id);
                _questiontrueorfalse.Save();

                return RedirectToAction("Edit", new { id = exid });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrueAnswers(CreateIndexExamViewModel viewModel , int AdminId , int PlantId)
        {
            try
            {
                foreach (var item in viewModel.TrueAnswer)
                {
                    Answer i = _answer.Entity.GetById(item);
                    i.boolAnswer = true;
                    _answer.Entity.UpDate(i);
                }
                _answer.Save();


                Plant pla = _plant.Entity.GetById(PlantId);

                ExamIndexViewModel exam = new ExamIndexViewModel();

                try
                {
                    exam.Exams = _exam.Entity.GetAll().Where(e => e.AdminIdFk == AdminId && e.Plant == pla).ToList();
                }
                catch
                {
                    exam.Exams = null;
                }
                try
                {
                    exam.IdAdmin = AdminId;
                }
                catch
                {
                    exam.IdAdmin = 0;
                }
                try
                {
                    exam.PlantId = PlantId;
                }
                catch
                {
                    exam.PlantId = 0;
                }
                return View("Index", exam);
            }
            catch
            {
                CreateIndexExamViewModel index = new CreateIndexExamViewModel();

                index.Examm = _exam.Entity.GetById(viewModel.ExamId);
                index.Questionss = _question.Entity.GetAll().Where(q => q.ExamIdFk == viewModel.ExamId).ToList();
                index.Answerss = _answer.Entity.GetAll().Where(a => a.Exam == index.Examm).ToList();
                ViewBag.AdminId = AdminId;
                ViewBag.PlantId = PlantId;

                return View("CreateIndex", index);
            }
            
        }

        public ActionResult TrueAnswers2(CreateIndexExamViewModel viewModel)
        {
            var ans = _answer.Entity.GetById(viewModel.FirstTrueAnswer);
            ans.boolAnswer = true;
            _answer.Entity.UpDate(ans);
            _answer.Save();
           
            return RedirectToAction("Edit", new { id = viewModel.ExamId });
              
        }

        public ActionResult Details(int id)
        {
            Exam exam = _exam.Entity.GetById(id);
            var question = _question.Entity.GetAll().Where(q => q.ExamIdFk == id).ToList();
            var answer = _answer.Entity.GetAll().Where(a => a.Exam == exam).ToList();
            var question_true_false = _questiontrueorfalse.Entity.GetAll().Where(q => q.ExamIdFk == id).ToList();
            DetailsExamViewModel viewModel = new DetailsExamViewModel();

            try
            {
                viewModel.Exam = exam;
            }
            catch
            {
                viewModel.Exam = null;
            }
            try
            {
                viewModel.Questions = question;
            }
            catch
            {
                viewModel.Questions = null;
            }
            try
            {
                viewModel.Question_True_Or_Falses = question_true_false;
            }
            catch
            {
                viewModel.Question_True_Or_Falses = null;
            }
            try
            {
                viewModel.Answers = answer;
            }
            catch
            {
                viewModel.Answers = null;
            }
            
            return View(viewModel);
        }

        public ActionResult Assent(int id , int plid)
        {
            Exam exam = _exam.Entity.GetById(id);

            Exam newexam = new Exam()
            {
                AdminIdFk = exam.AdminIdFk,
                Assent = true,
                Title = exam.Title,
                Plant = _plant.Entity.GetById(plid),
                Questions = _question.Entity.GetAll().Where(q => q.ExamIdFk == id).ToList(),
                question_True_Or_Falses = _questiontrueorfalse.Entity.GetAll().Where(w => w.ExamIdFk == id).ToList(),
                answers = _answer.Entity.GetAll().Where(a => a.Exam == exam).ToList(),
            };

            _exam.Entity.Insert(newexam);
            _exam.Entity.Delete(id);
            _exam.Save();
            return RedirectToAction("Order", "Admin");
        }

        public ActionResult DisplayExam(int id , int plid)
        {
            Exam exam = _exam.Entity.GetById(id);
            DetailsExamViewModel viewModel = new DetailsExamViewModel();

            try
            {
                viewModel.Exam = exam;
            }
            catch
            {
                viewModel.Exam = null;
            }
            try
            {
                viewModel.Questions = _question.Entity.GetAll().Where(q => q.ExamIdFk == exam.ExamId).ToList();
            }
            catch
            {
                viewModel.Questions = null;
            }
            try
            {
                viewModel.Answers = _answer.Entity.GetAll().Where(a => a.Exam == exam).ToList();
            }
            catch
            {
                viewModel.Answers = null;
            }
            try
            {
                viewModel.Question_True_Or_Falses = _questiontrueorfalse.Entity.GetAll().Where(q => q.ExamIdFk == exam.ExamId).ToList();
            }
            catch
            {
                viewModel.Question_True_Or_Falses = null;
            }
            try
            {
                viewModel.Admin = _admin.Entity.GetById(exam.AdminIdFk);
            }
            catch
            {
                viewModel.Admin = null;
            }
            try
            {
                viewModel.PlantId = plid;
            }
            catch
            {
                viewModel.PlantId = 0;
            }
            return View(viewModel);
        }

    
        public ActionResult DisplayExam2(DetailsExamViewModel viewModel)
        {
            AnswerUserViewModel Mod = new AnswerUserViewModel();

            int Question_Count = 0;
            int Answer1 = 0;
            int[] ar = new int[viewModel.True_Answer.Where(a => a != -2000).Count()];
            int[] ansfalse = new int[viewModel.True_Answer.Where(a => a != -2000).Count()];
            if (viewModel.True_Answer != null)
            {
                foreach (var item in viewModel.True_Answer)
                {
                    if (item == -2000)
                    {
                        Answer1++;
                    }
                    else
                    {
                        ar[Question_Count] = _answer.Entity.GetById(item).QuestionId;
                        ansfalse[Question_Count] = item;
                        Question_Count++;
                    }
                }
            }

            Mod.QuestionId = ar.ToList();
            Mod.AnId = ansfalse.ToList();
            if(viewModel.True_Answer_2 != null)
            {

                int count = 0;
                int Question_Count_true_false = 0;
                int[] arr = new int[viewModel.True_Answer_2.Where(a => a != -4000).Count()];
                foreach (var qu in viewModel.True_Answer_2)
                {
                    try
                    {
                        if (viewModel.True_Answer_2[count] == -4000)
                        {
                            Answer1++;
                        }
                        else
                        {
                            arr[Question_Count_true_false] = qu;
                            Question_Count_true_false++;
                        }
                    }
                    catch
                    {

                    }
                    count++;
                }
                Mod.Question_trueOrFalseId = arr.ToList();
            }


            Exam exam = _exam.Entity.GetById(viewModel.Exam_Id);

             int A = Answer1;
             int Q = _question.Entity.GetAll().Where(q => q.ExamIdFk == exam.ExamId).Count()
                               + _questiontrueorfalse.Entity.GetAll().Where(q => q.ExamIdFk == exam.ExamId).Count();

            int Resalt = (A * 100) / Q;
            
            Mod.AdminName = viewModel.AdminName;
            Mod.ExamId = viewModel.Exam_Id;
            Mod.ExamName = exam.Title;
            Mod.FirstName = viewModel.FirstName;
            Mod.LastName = viewModel.LastName;
            Mod.Level = _level.Entity.GetById(_plant.Entity.GetById(viewModel.PlantId).LevelIdFk).LevelName;
            Mod.PlantName = _plant.Entity.GetById(viewModel.PlantId).PlantName;
            Mod.Result = Resalt;
            Mod.Spe = _spe.Entity.GetById(_plant.Entity.GetById(viewModel.PlantId).SpecialtiesIdFk).SpecialtiesName;

            if(Resalt < 50)
            {
                Mod.m = "راسب";
            }
            else if(Resalt >= 50 && Resalt < 65)
            {
                Mod.m = "مقبول";
            }
            else if(Resalt >= 65 && Resalt < 75)
            {
                Mod.m = "جيد";
            }
            else if(Resalt >= 75 && Resalt < 85)
            {
                Mod.m = "جيد جدا";
            }
            else if(Resalt >= 85 && Resalt <= 100)
            {
                Mod.m = "ممتاز";
            }

            return View(Mod);
        }

        public ActionResult Details_false(AnswerUserViewModel viewModel)
        {
            DetailsExamViewModel mod = new DetailsExamViewModel();
            int i = 0;
           
            if(viewModel.QuestionId != null)
            {
                Question[] question = new Question[viewModel.QuestionId.Count()];

                foreach (var item in viewModel.QuestionId)
                {
                    question[i] = _question.Entity.GetById(item);
                    i++;
                }
                mod.Questions = question.ToList();
            }

            i = 0;

            if(viewModel.Question_trueOrFalseId != null)
            {
                Question_True_or_false[] question_True_Or_False = new Question_True_or_false[viewModel.Question_trueOrFalseId.Count()];

                foreach (var item2 in viewModel.Question_trueOrFalseId)
                {
                    question_True_Or_False[i] = _questiontrueorfalse.Entity.GetById(item2);
                    i++;
                }
                mod.Question_True_Or_Falses = question_True_Or_False.ToList();
            }
         
            i = 0;

            if(viewModel.AnId != null)
            {
                int[] anf = new int[viewModel.AnId.Count()];

                foreach (var item3 in viewModel.AnId)
                {
                    anf[i] = item3;
                    i++;
                }
                mod.False_1 = anf.ToList();
            }
           
            if (viewModel.QuestionId != null)
            {
                mod.Answers = _answer.Entity.GetAll().Where(a => a.Exam == _exam.Entity.GetById(mod.Questions[0].ExamIdFk)).ToList();
            }

            mod.FirstName = viewModel.FirstName;
            return View(mod);
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
