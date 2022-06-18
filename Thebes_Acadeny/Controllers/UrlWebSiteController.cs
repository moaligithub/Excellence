using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;
using Thebes_Acadeny.Models.Entity;
using Thebes_Acadeny.ViewModel;

namespace Thebes_Acadeny.Controllers
{
    public class UrlWebSiteController : Controller
    {
        private readonly IUnitOfWork<UrlWebSite> _url;

        public UrlWebSiteController(IUnitOfWork<UrlWebSite> url)
        {
            _url = url;
        }
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                UrlViewModel viewModel = new UrlViewModel();
                try
                {
                    viewModel.Url = _url.Entity.GetAll().FirstOrDefault().UrlText;
                }
                catch
                {
                    viewModel.Url = null;
                }
                try
                {
                    viewModel.UrlId = _url.Entity.GetAll().FirstOrDefault().UrlId;
                }
                catch
                {
                    viewModel.UrlId = 0;
                }
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }    
            
        }
        public ActionResult Edit(UrlViewModel viewModel)
        {
            if(HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                if(viewModel.NewUrl != null)
                {
                    UrlWebSite newUrl = new UrlWebSite
                    {
                        UrlId = viewModel.UrlId,
                        UrlText = viewModel.NewUrl
                    };
                    _url.Entity.UpDate(newUrl);
                    _url.Save();
                }
                return RedirectToAction("ControlOwner112004", "LoginControl");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
