using RWA_User.App_Code;
using RWA_User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RWA_User.Controllers
{
    public class ProfilController : BaseController
    {
        // GET: Profil
        [Authorize]
        public ActionResult Profil()
        {
            Profil profil = new Profil();
            profil.Employee = Repo.GetEmployee(int.Parse(Session["IDEmployee"].ToString()));
            profil.Team = Repo.GetTeam(profil.Employee.AssignedTeam);
            return View(profil);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Profil(Profil profil)
        {
            profil.Employee = Repo.GetEmployee(int.Parse(Session["IDEmployee"].ToString()));
            profil.Team = Repo.GetTeam(profil.Employee.AssignedTeam);
            if (ModelState.IsValid)
            {
                profil.User = new User();
                profil.User.Password = profil.Password;
                if(Repo.ChangePassword(profil.Employee.Email, profil.User.Password))
                {
                    TempData["PasswordChangeStatus"] = $"Password uspješno promijenjen";
                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["PasswordChangeStatus"] = $"Nekaj ne valja, nisam mogao promijeniti password";
                    return Redirect("/Home/Index");
                }
                
            }
            return View(profil);
        }
    }
}