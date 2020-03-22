using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EventManager.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
          
        public ActionResult RecoveryPassword()
        {
            var login = new Login
            {
                Username = Session["Username"] != null ? (string)Session["Username"] : string.Empty
            };

            return View(login);
        }
        public ActionResult Register()
        {
            return View();
        }

        public bool LoginUser(Login login)
        {
            Session["Username"] = login.Username;
           
           User user;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                user = (from u in context.User
                        where u.Email.Equals(login.Username) && u.Password.Equals(login.Password)
                        select u).FirstOrDefault();


                FormsAuthentication.SetAuthCookie(login.Username, false);
                
                //
                 
                string loginData = JsonConvert.SerializeObject(login);

                var authTicket = new FormsAuthenticationTicket(1, login.Username, DateTime.Now,
                                                       DateTime.Now.AddMinutes(30), true, loginData);
 
                string cookieContents = FormsAuthentication.Encrypt(authTicket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieContents)
                                {
                                    Expires = authTicket.Expiration,
                                    Path = FormsAuthentication.FormsCookiePath
                                };
                if (Response != null)
                {
                    Response.Cookies.Add(cookie);
                }

                 
                var principal = Thread.CurrentPrincipal;


                return user != null;
            }  
        }

        public void Logout()
        {
            FormsAuthentication.SignOut(); 
        }
    }
}