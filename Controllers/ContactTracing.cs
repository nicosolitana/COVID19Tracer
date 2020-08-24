//--------------------------------------------------------------------------
// @author:  Nico Solitana, Christian Kevin Villanueva
// @subject: Advanced Operating System
// @course:  MS Computer Science
// @university: De La Salle University - Manila
//--------------------------------------------------------------------------

using SerialSimulation.Models;
using System.Collections.Generic;
using System.Linq;

namespace SerialSimulation.Controllers
{
    public class ContactTracing
    {
        public int GetPlacesVisitedCount(List<TracerData> infdata)
        {
            List<string> places = new List<string>();
            foreach (TracerData tdata in infdata)
            {
                places.Add(tdata.History.Location);
            }
            return places.Distinct().Count();
        }

        public int GetDaysTravelledCount(List<TracerData> infdata)
        {
            List<string> dateData = new List<string>();
            foreach (TracerData tdata in infdata)
            {
                dateData.Add(tdata.History.dateData);
            }
            return dateData.Distinct().Count();
        }

        public int GetAffectedCommunitiesCount(List<Person> infdata)
        {
            List<string> communities = new List<string>();
            foreach (Person pdata in infdata)
            {
                communities.Add(pdata.address);
            }
            return communities.Distinct().Count();
        }

        public List<Person> GetSecondLevel(List<Person> names, List<TracerData> _tracedData)
        {
            List<Person> lstIdentified = new List<Person>();
            foreach (var pps in names)
            {
                List<Activities> actLst = new List<Activities>();
                actLst = GetSecondLevelActivities(pps, _tracedData);
                foreach (Activities s in actLst)
                {
                    List<Person> ifPeeps = new List<Person>();
                    ifPeeps = GetSecondLevelPeople(s, _tracedData);
                    lstIdentified.AddRange(ifPeeps);
                }
            }
            return lstIdentified;
        }

        private List<Activities> GetSecondLevelActivities(Person pps, List<TracerData> _tracedData)
        {
            List<Activities> actLst = new List<Activities>();
            foreach (TracerData x in _tracedData)
            {
                if (x.Name.firstName == pps.firstName
                    && x.Name.lastName == pps.lastName)
                {
                    actLst.Add(new Activities()
                    {
                        dateData = x.History.dateData,
                        timeData = x.History.timeData,
                        Location = x.History.Location
                    });
                }
            }
            return actLst;
        }

        private List<Person> GetSecondLevelPeople(Activities s, List<TracerData> _tracedData)
        {
            List<Person> ifPeeps = new List<Person>();
            foreach (TracerData x in _tracedData)
            {
                if (x.History.dateData == s.dateData
                    && x.History.timeData == s.timeData
                    && x.History.Location == s.Location)
                {
                    ifPeeps.Add(new Person()
                    {
                        firstName = x.Name.firstName,
                        lastName = x.Name.lastName,
                        address = x.Name.address
                    });
                }
            }
            return ifPeeps;
        }
    }
}
