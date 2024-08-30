using Microsoft.AspNetCore.Mvc;

namespace WebApiTrain.Api.Controllers
{
    public class ReportController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
