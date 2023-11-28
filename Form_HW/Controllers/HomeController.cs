using Form_HW.Models;
using Form_HW.Models.Home;
using Form_HW.Services.Valid;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;


namespace Form_HW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IValidationService _valid;

        public HomeController(ILogger<HomeController> logger, IValidationService valid)
        {
            _logger = logger;
            _valid = valid;
        }

        public ViewResult Index()
        {
            FormModel? formModel;
            FormValidModel? validModel = new ();

            if (HttpContext.Session.Keys.Contains("formModel"))
            {
                
                formModel = JsonSerializer.Deserialize<FormModel>(
                    HttpContext.Session.GetString("formModel")!
                );
                HttpContext.Session.Remove("formModel");

                validModel.IsNameValid = _valid.IsNameValid(formModel!.FistName);
                validModel.IsLastameValid = _valid.IsNameValid(formModel!.LastName);
                validModel.IsPhoneValid = _valid.IsPhoneValid(formModel!.Phone);
                validModel.IsEmailValid =_valid.IsEmailValid(formModel!.Email);
                validModel.FormData = formModel;
            }else
            {
                validModel.FormData = new();
            }
           

            return View(validModel);
        }

        [HttpPost]
        public IActionResult ProcessTransferForm(FormModel? formModel)
        {
            
            if (formModel != null)
            {
                HttpContext.Session.SetString(
                    "formModel",
                    JsonSerializer.Serialize(formModel)
                );
            }
            return RedirectToAction(nameof(Index));
        }
        


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}