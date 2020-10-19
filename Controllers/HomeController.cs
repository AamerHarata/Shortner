using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shortner.Data;
using Shortner.Models;

namespace Shortner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("/{id?}")]
        public IActionResult Index(string id)
        {
            
            if(string.IsNullOrWhiteSpace(id))
                return View();

            var url = _context.Urls.SingleOrDefault(x => x.Id == id);

            if (url == null)
            {
                return RedirectToAction("Index", new {id = ""});
            }
            
            return Redirect(url.Link);
        }

        public IActionResult Privacy()
        {
            var list = _context.Urls.ToList();
            return View(list);
        }


        public IActionResult SaveUrl(string url)
        {
            url = url.Trim().ToLower();
            if(!url.Contains("https://"))
                url = $"https://{url}";
            var existing = _context.Urls.SingleOrDefault(x => x.Link == url);
            if (existing != null)
                return Ok(existing.Id);
            
            var uniqueId = Guid.NewGuid().ToString().Split("-")[0];
            
            
            var newUrl = new Url(){Id = uniqueId, Link = url};

            _context.Add(newUrl);
            _context.SaveChanges();
                        
            return Ok(uniqueId);
        }
        
        
        
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
