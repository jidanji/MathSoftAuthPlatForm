using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: Admin/UserInfo
        public ActionResult Index()
        {
            return new RedirectResult("/Admin/Login");
        }
    }
}