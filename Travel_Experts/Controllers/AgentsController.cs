using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel_Experts.Models;

namespace Travel_Experts.Controllers
{
    public class AgentsController : Controller
    {
        // GET: AgentsController
        public ActionResult AgentList()
        {
            List<Agent> agents = null;
            List<Agency> agencies = null;
            try
            {
                agents = AgentsManager.GetAgents();
                agencies = AgentsManager.GetAgencies();
                ViewBag.Agencies = agencies;
                ViewBag.Message = "";
            }
            catch (Exception)
            {
                ViewBag.Message = "Database error getting Contact Agents data.";
            }
            return View(agents);
        }
    

        // GET: AgentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AgentsController/Create
        public ActionResult Create()
        {
            return View();
        }

    }//end class
}//end namespace
