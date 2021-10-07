/* Manager class for Contact Agents
 * Author: Priya
 * Date: 06-oct-2021
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Travel_Experts.Models
{
    public class AgentsManager
    {
        public static List<Agent> GetAgents()
        {
            List<Agent> agents;
            TravelExpertsContext db = new TravelExpertsContext();
            agents = db.Agents.Include(ag=>ag.Agency).ToList();
            return agents;
     
        }//end list

        public static List<Agency> GetAgencies()
        {
            List<Agency> agencies;
            TravelExpertsContext db = new TravelExpertsContext();
            agencies = db.Agencies.ToList();
            return agencies;

        }//end list
    }
}
