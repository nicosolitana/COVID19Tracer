//--------------------------------------------------------------------------
// @author:  Nico Solitana, Christian Kevin Villanueva
// @subject: Advanced Operating System
// @course:  MS Computer Science
// @university: De La Salle University - Manila
//--------------------------------------------------------------------------

namespace SerialSimulation.Models
{
    public class Activities
    {
        private string _date;
        public string dateData
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        private string _time;
        public string timeData
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
            }
        }


        private string _location;
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }
    }
}
