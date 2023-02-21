using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using MAZIKONG.App_Start;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class MainController : AdminBaseController
    {
        [PreLoadMenuList]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}