using Microsoft.AspNetCore.Mvc;

namespace GestionReservation.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/ChooseLoginType
        public IActionResult ChooseLoginType()
        {
            return View();
        }
    }
}
