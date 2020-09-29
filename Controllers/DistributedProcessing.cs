using COVID19Tracer.Models;
using Newtonsoft.Json;
using RestSharp;
using SerialSimulation;
using SerialSimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace COVID19Tracer.Controllers
{
    class DistributedProcessing
    {
        private static List<Person> secLvl = new List<Person>();
        private static List<TracerData> firstLvl = new List<TracerData>();
        private static List<Activities> actList = new List<Activities>();
        public int peopleCount = 0;
        public int placesCount = 0;
        public int daysCount = 0;
        public int comCount = 0;
        public int secondLevelCount = 0;

        public void StartDataProcessing(string fname, string lname)
        {
            GetAllData(fname, lname);
            var fsNoDups = firstLvl.Distinct(new TraceDataComparer()).ToArray();
            var scNoDups = secLvl.Distinct(new PersonNameComparer()).ToArray();
            var actLstGroups = actList.GroupBy(x => x.Location);

            secondLevelCount = scNoDups.Count();
            peopleCount = fsNoDups.Count();
            placesCount = actLstGroups.Count();
            daysCount = fsNoDups.GroupBy(x => x.History.dateData).Count();
            comCount = scNoDups.GroupBy(x => x.address).Count();
        }


        //RESTSHARP
        private static void GetAllData(string fname, string lname)
        {
            List<string> url = new List<string>();
            url.Add("http://server01-covid19tracer-api.io/api/");
            url.Add("https://server02-covid19tracer-api.azurewebsites.net/api/");
            url.Add("https://server03-covid19tracer-api.azurewebsites.net/api/");
            url.Add("https://server04-covid19tracer-api.azurewebsites.net/api/");
            url.Add("https://server05-covid19tracer-api.azurewebsites.net/api/");
            url.Add("https://server06-covid19tracer-api.azurewebsites.net/api/");

            Parallel.ForEach(url, (item) =>
            {
                GetResult(item, fname, lname);
            });
        }

        private static void GetResult(string path, string fname, string lname)
        {
            ResultSet rs = new ResultSet();
            RestClient client = new RestClient(path);
            client.Timeout = 1800000;
            string resource = "GetInfection?_fname=" + fname + "&_lname=" + lname + "";

            var request = new RestRequest(resource, DataFormat.Json);
            var response = client.Execute(request) as RestResponse;
            if (response != null && ((response.StatusCode == HttpStatusCode.OK) &&
                (response.ResponseStatus == RestSharp.ResponseStatus.Completed))) 
            {
                rs = JsonConvert.DeserializeObject<ResultSet>(response.Content);
                secLvl.AddRange(rs.SecLvlTraces);
                firstLvl.AddRange(rs.FirstLvlTraces);
                actList.AddRange(rs.ActList);
            }
        }
    }
}
