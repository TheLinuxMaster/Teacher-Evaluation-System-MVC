using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teacher_Evaluation.Models;
using Teacher_Evaluation.View_Models;

namespace Teacher_Evaluation.Controllers
{
    public class MainController : Controller
    {
        EvaluationDataContext db = new EvaluationDataContext();
        // GET: Main
        public ActionResult Index()
        {
            EVModel ev = new EVModel();
            ev.questions = db.Questions.ToList();
            ev.courses = db.Sections.Where(s=>s.SId==1).Single().Courses.ToList();
            return View(ev);
        }
        public ActionResult Result()
        {
            return View(new EVModel
            {
                questions = db.Questions.ToList(),
                courses = db.Sections
                    .Where(s => s.SId == 1).Single().Courses
                    .Where(c => c.CId == 1)
            });
        }

        [HttpPost]
        public ActionResult ParticipantSave(String[] evaluation)
        {


            DateTime dt = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Participants.Add(new Participant
                {
                    Date = dt

                });
                            
             
                db.SaveChanges();
                foreach (var item in evaluation)
                {
                    string[] str = item.Split(':');
                    db.Evaluations.Add(new Evaluation
                    {
                        CId = int.Parse(str[0]),
                        QId = int.Parse(str[1]),
                        Grade = str[2],
                        PId =int.Parse(db.Participants.Max(p=>p.PId.ToString()))               
                    });
                }
              
                db.SaveChanges();

                return Content("Suncess!");
            }
        
            return Content("Fail");
        }
    }
}