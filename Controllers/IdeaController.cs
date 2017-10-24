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
    public class IdeaController : Controller
    {
        private  BrightIdeasContext _context;
        public IdeaController(BrightIdeasContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/bright_ideas")]
        public IActionResult bright_ideas()
        {
            int? user_id = HttpContext.Session.GetInt32("user_id");
            if(user_id != null) {
                // Set current user for nav bar...
                ViewBag.name = HttpContext.Session.GetString("name");

                // // GET from DB...
                List<Idea> allIdeas = _context.ideas.Include( i => i.user ).Include(i => i.likes).OrderByDescending( i => i.likes.Count ).ToList();

                ViewBag.allIdeas = allIdeas;
                ViewBag.user_id = user_id;

                return View();
            } else {
                return View("Index");
            }
        }
        // Handle the creating an idea
        [HttpPost]
        [Route("/bright_ideas")]
        public IActionResult LikeIdea( IdeaVal idea) {
            int? user_id = HttpContext.Session.GetInt32("user_id");

            if( ModelState.IsValid ) {
                Idea NewIdea = new Idea {
                    userid = (int)user_id,
                    content = idea.content
                };

                // insert to DB...
                _context.ideas.Add(NewIdea);
                _context.SaveChanges();

                return RedirectToAction("bright_ideas");
            } else {

                // NOTE: may need to populate viewbag and show val errors (ignoring because not expressly asked for)
                return RedirectToAction("bright_ideas");
            }
        }

        // Handle the liking of an idea
        [HttpPost]
        [Route("/bright_ideas/{id}")]
        public IActionResult LikeIdea( int id ) {
            int? user_id = HttpContext.Session.GetInt32("user_id");

            // NOTE:
            // * There was a conflict with the discription, so I followed the requirement on the bottom
            // * to only allow one(1) like for an idea per user!

            // check if user has already liked this idea...
            List<Like> alreadyLiked= _context.likes.Where(l => l.userid == user_id && l.ideaid == id).ToList();
            if(alreadyLiked.Count > 0) {
                return RedirectToAction("bright_ideas");
            }

            // add new attending user...
            Like NewLike = new Like {
                userid = (int)user_id,
                ideaid = id,
            };

            _context.likes.Add(NewLike);
            _context.SaveChanges();

            return RedirectToAction("bright_ideas");
        }

        [HttpGet]
        [Route("/bright_ideas/{id}")]
        public IActionResult ShowIdea(int id){
            int? user_id = HttpContext.Session.GetInt32("user_id");
            if(user_id != null) {
                // GET from DB...
                Idea currentIdea = _context.ideas.Include( i => i.user ).Include( i => i.likes ).ThenInclude(i => i.user ).SingleOrDefault(i => i.ideaid == id);

                ViewBag.currentIdea = currentIdea;
                ViewBag.user_id = user_id;

                return View();
            } else {
                return View("Index");
            } 
        }

        [HttpPost]
        [Route("/bright_ideas/{id}/delete")]
        public IActionResult DeleteIdea(int id){
            int? user_id = HttpContext.Session.GetInt32("user_id");

            // GET from DB...
            Idea targetIdea = _context.ideas.Where(i => i.ideaid == id).SingleOrDefault();

            _context.ideas.Remove(targetIdea);
            _context.SaveChanges();

            return RedirectToAction("bright_ideas");
        }
    }
}
