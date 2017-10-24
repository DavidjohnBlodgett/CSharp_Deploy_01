using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CSharp_belt_exam_project_two.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CSharp_belt_exam_project_two.Controllers
{
    public class HomeController : Controller
    {
        private  BrightIdeasContext _context;
        public HomeController(BrightIdeasContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
