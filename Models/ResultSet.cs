using SerialSimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID19Tracer.Models
{
    public class ResultSet
    {
        public List<TracerData> FirstLvlTraces { get; set; }
        public List<Person> SecLvlTraces { get; set; }
        public List<Activities> ActList { get; set; }
    }
}
