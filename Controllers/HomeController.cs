using FYPManagment.Models;
using FYPManagment.ViewModel;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FYPManagment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public HomeController()
        {
            _Context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var groups = _Context.GroupMembers.Include(g => g.Group).Where(u => u.MemberId == userId).Select(a => a.Group).ToList();

            var ViewModel = new HomeViewModel
            {
                Groups = groups              
            };
            
            return View(ViewModel);
        }





        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}