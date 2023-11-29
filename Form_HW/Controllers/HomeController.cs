using EF_Form_HW.Data.Entities;
using EF_Form_HW.Models;
using EF_Form_HW.Models.Home;
using EF_Form_HW.Services.Valid;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;


namespace EF_Form_HW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _db;
        private readonly expandingMethods _expMethods;
        IValidationService _valid;

        public HomeController(ILogger<HomeController> logger, IValidationService valid, 
            DataContext dataContext, expandingMethods expMethods)
        {
            _expMethods = expMethods;
            _db = dataContext;
            _logger = logger;
            _valid = valid;
        }

        public async Task<IActionResult> Index()
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

                if (_expMethods.validModelFildsChecker(validModel))
                {
                    User user = new();
                    user.Name = formModel.FistName;
                    user.LastName = formModel.LastName;
                    user.Phone = formModel.Phone;
                    user.Email = formModel.Email;
                    user.RegisterTime = DateTime.Now;

                   await _db.Users.AddAsync(user);
                    _db.SaveChanges();

                    return RedirectToAction(nameof(BaseTable));
                }
            }else
            {
                validModel.FormData = new();
            }

            return View(validModel);
        }

        //napisat formu i sdelat tablicu
        public async Task<ViewResult> BaseTable()
        {
            
            return View(await _db.Users.ToListAsync<User>());
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