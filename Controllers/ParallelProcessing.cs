//--------------------------------------------------------------------------
// @author:  Nico Solitana, Christian Kevin Villanueva
// @subject: Advanced Operating System
// @course:  MS Computer Science
// @university: De La Salle University - Manila
//--------------------------------------------------------------------------

using SerialSimulation.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerialSimulation.Controllers
{
    public class ParallelProcessing
    {
        public List<TracerData> TracerData = new List<TracerData>();
        public int peopleCount = 0;
        public int placesCount = 0;
        public int daysCount = 0;
        public int comCount = 0;

        public void StartDataProcessing(string fname, string lname, List<TracerData> _tracerData)
        {
            int count = _tracerData.Where(x => x.Name.firstName == fname && x.Name.lastName == lname).Count();
            if (count > 0)
            {
                GatherData(_tracerData, fname, lname);
            }
        }

        private void GatherData(List<TracerData> _tracerData, string fname, string lname)
        {
            ContactTracing conTracer = new ContactTracing();
            var dataRet = _tracerData.Where(x => x.Name.firstName == fname && x.Name.lastName == lname)
                        .Select(i => new TracerData() { Name = i.Name, History = i.History }).ToList();

            List<Person> infectedPeeps = new List<Person>();
            infectedPeeps = TraceInfection(conTracer, _tracerData, dataRet);
            var noDupes = infectedPeeps.Distinct(new PersonNameComparer()).ToArray();
            peopleCount = noDupes.Count();
            placesCount = conTracer.GetPlacesVisitedCount(dataRet);
            daysCount = conTracer.GetDaysTravelledCount(dataRet);
            comCount = conTracer.GetAffectedCommunitiesCount(infectedPeeps);
        }

        private List<Person> TraceInfection(ContactTracing conTracer, List<TracerData> _tracerData, List<TracerData> dataRet)
        {
            List<Person> infectedPeeps = new List<Person>();

            Parallel.ForEach(dataRet, (s) => {
                var infPerson = _tracerData.Where(x => x.History.dateData == s.History.dateData && x.History.timeData == s.History.timeData && x.History.Location == s.History.Location)
                        .Select(a => new Person() { firstName = a.Name.firstName, lastName = a.Name.lastName, address = a.Name.address }).ToList();

                var secLvlInfection = conTracer.GetSecondLevel(infPerson, _tracerData);
                infectedPeeps.AddRange(secLvlInfection);
            });
            return infectedPeeps;
        }
    }
}
