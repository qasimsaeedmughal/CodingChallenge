 
using CASS_Portal.Common;
using CASS_Portal.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CASS_Portal.Controllers
{
    public class HomeController : Controller
    {
       
       
        private readonly ILogger<HomeController> _logger;
        public IExternalAPICalling _IExternalAPICalling; 
        public IHttpContextAccessor  _IHttpContextAccessor; 
         
        public HomeController(ILogger<HomeController> logger, IExternalAPICalling externalAPICalling, IHttpContextAccessor httpContextAccessor)
        { 
            _logger = logger;
            _IExternalAPICalling = externalAPICalling;
            _IHttpContextAccessor = httpContextAccessor;
        
        }

        public IActionResult Index()
        {
            var response =  _IExternalAPICalling.GeRandomtQuote("GetRandomQuote");
            Quoteable? quoteable =null;
            if (!string.IsNullOrEmpty(response.Data))
            {
                quoteable = JsonConvert.DeserializeObject<Quoteable?>(response.Data);

            }
           // _IHttpContextAccessor.HttpContext.Response.WriteAsync("<script>alert('Data inserted successfully')</script>");

            return View(quoteable);
        }

        public IActionResult AlbertEinstein()
        {
            var response = _IExternalAPICalling.GeRandomtQuote("GetQuotesByAuthorName");
            var data = JsonConvert.DeserializeObject<BulkQuotes>(response.Data);
            return View(data);
            
        }

        public  IActionResult ShowShipper()
        {
            var CurrentPath = _IHttpContextAccessor.HttpContext.Request.Path.ToString();
            if (CurrentPath.ToLower().Contains("showshipper") && CurrentPath != "/")
            {
                if (string.IsNullOrEmpty(_IHttpContextAccessor.HttpContext.Session.GetString("USER")))
                {
                    //_IHttpContextAccessor.HttpContext.Response.Redirect("/login");
                   return Redirect("/home/login");

                }
            } 
            var response = _IExternalAPICalling.GeRandomtQuote("GetAllShipers");
            var data = JsonConvert.DeserializeObject<List<Shipper>>(response.Data);
            return View(data);
        }


        [Route("{id}")]
        public IActionResult ShowShipmentDetail(int id)
        {
            if (id!=0)
            {
                var response = _IExternalAPICalling.GeRandomtQuote($"GetShipmentsbyShipperID/{id}");
                var data = JsonConvert.DeserializeObject<List<ShipperShipmentDetail>>(response.Data);
                return View(data);
            }
            return View();
        }

        public IActionResult login()
        { 
            return View();
        }
        [HttpPost]
        public IActionResult loginMethod(string Name, string password)
        {
            if (Name=="admin" && password =="admin")
            {
                _IHttpContextAccessor.HttpContext.Session.SetString("USER","USER");
                TempData["Message"] = "";

                return Redirect("/home/showshipper");
            }
            else
            {
                TempData["Message"] = "Enter Proper Credentials";
            }

            return Redirect("/home/login");
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