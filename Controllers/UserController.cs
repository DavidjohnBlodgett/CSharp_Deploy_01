using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CSharp_belt_exam_project_two.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CSharp_belt_exam_project_two.Controllers {
    public class UsersController : Controller {

        private BrightIdeasContext _context;
        public UsersController(BrightIdeasContext context)
        {
            _context = context;
        }
    
        [HttpPost]
        [Route("/users")]
        public IActionResult Users(UserVal user) {  

            if( ModelState.IsValid ) {
                User NewUser = new User {
                    name = user.name,
                    alias = user.alias,
                    email = user.email,
                    password = user.password
                };

                // get from db...
                List<User> usr = _context.users.Where(userItem => userItem.email == NewUser.email).ToList();

                // check if user already exists...
                if(usr.Count > 0){
                    // need to render view to support model binded errors...
                    ModelState.AddModelError("email","User with that email already exists!");
                    return View("Index");
                }

                // insert to DB...
                _context.users.Add(NewUser);
                _context.SaveChanges();

                // set session... by getting the created user id...
                List<User> CurrentUser = _context.users.Where(userItem => userItem.email == NewUser.email).ToList();
                HttpContext.Session.SetString("name", (string)CurrentUser[0].name);
                HttpContext.Session.SetInt32("user_id", (int)CurrentUser[0].userid);

                return RedirectToAction("bright_ideas", "Idea");    
            } else {
                // need to render view to support model binded errors...
                return View("Index");
            }
        }

        [HttpPost]
        [Route("/users/login")]
        public IActionResult UserLogin(LoginVal user) {

            if( ModelState.IsValid ) {
                // get from db...
                List<User> usr = _context.users.Where(userItem => userItem.email == user.email && userItem.password == user.password ).ToList();

                if(usr.Count > 0){
                    // set session...
                    HttpContext.Session.SetString("name", (string)usr[0].name);
                    HttpContext.Session.SetInt32("user_id", (int)usr[0].userid);
                    return RedirectToAction("bright_ideas", "Idea");
                } else {
                    ModelState.AddModelError("email","No user with that email found or password does not match!");
                    return View("Index");
                }
            } else {
                // need to render view to support model binded errors...
                return View("Index");
            }
        }

        [HttpGet]
        [Route("/users/{id}")]
        public IActionResult ShowUser(int id) {
            int? user_id = HttpContext.Session.GetInt32("user_id");
            if(user_id != null) {
                // GET from DB...
                User currentUser = _context.users.Include( u => u.posts ).Include(u => u.likes ).SingleOrDefault(a => a.userid == id);

                ViewBag.currentUser = currentUser;
                ViewBag.user_id = user_id;

                return View();
            } else {
                return View("Index");
            }
        }

        [HttpGet]
        [Route("/users/logoff")]
        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}