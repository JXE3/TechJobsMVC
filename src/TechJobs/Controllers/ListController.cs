using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class ListController : Controller
    {
        internal static Dictionary<string, string> columnChoices = new Dictionary<string, string>();

        // This is a "static constructor" which can be used
        // to initialize static members of a class
        static ListController() 
        {
            
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");
        }

        public IActionResult Index()
        {
            ViewBag.columns = columnChoices;
            return View();
        }

        public IActionResult Values(string column)
        {
            if (column.Equals("all"))
            {
                List<Dictionary<string, string>> jobs = JobData.FindAll();

                List<KeyValuePair<string, string>> jobsKVP = new List<KeyValuePair<string, string>>();

                foreach (Dictionary<string, string> job in jobs)
                {
                    foreach (string key in job.Keys)
                    {
                        string thisValue = job[key];
                        jobsKVP.Add(new KeyValuePair<string, string>(key, thisValue));
                    }

                }

                 

                ViewBag.title =  "All Jobs";
                ViewBag.jobsKVP = jobsKVP;
            
                
                return View("Jobs");
            }
            else
            {
                List<string> items = JobData.FindAll(column);
                ViewBag.title =  "All " + columnChoices[column] + " Values";

                string cc = columnChoices[column];

                ViewBag.column = column;
                ViewBag.items = items;
                return View();
            }
        }

        public IActionResult Jobs(string column, string value)
        {
            List<Dictionary<String, String>> jobs = JobData.FindByColumnAndValue(column, value);


            List<KeyValuePair<string, string>> jobsKVP = new List<KeyValuePair<string, string>>();

            foreach (Dictionary<string, string> job in jobs)
            {
                foreach (string key in job.Keys)
                {
                    string thisValue = job[key];
                    jobsKVP.Add(new KeyValuePair<string, string>(key, thisValue));
                }
            }


            ViewBag.title = "Jobs with " + columnChoices[column] + ": " + value;
            ViewBag.jobsKVP = jobsKVP;


            return View();
        }
    }
}
