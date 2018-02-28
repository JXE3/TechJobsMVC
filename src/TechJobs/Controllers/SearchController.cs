using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            return View();
        }

        // TODO #1 - Create a Results action method to process 
        // search request and display results
        public IActionResult Results(string searchType, string searchTerm)
        {
            List<Dictionary<string, string>> jobs;
            if (searchType.Equals("all"))
                { jobs = JobData.FindByValue(searchTerm); }
            else
                { jobs = JobData.FindByColumnAndValue(searchType, searchTerm); } 
           

            List<KeyValuePair<string, string>> jobsKVP = new List<KeyValuePair<string, string>>();

            foreach (Dictionary<string, string> job in jobs)
            {
                foreach (string key in job.Keys)
                {
                    string thisValue = job[key];
                    jobsKVP.Add(new KeyValuePair<string, string>(key, thisValue));
                }

            }




            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            ViewBag.jobsKVP = jobsKVP;

            return View("Index");
            
        }

    }
}
