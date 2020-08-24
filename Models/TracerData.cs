//--------------------------------------------------------------------------
// @author:  Nico Solitana, Christian Kevin Villanueva
// @subject: Advanced Operating System
// @course:  MS Computer Science
// @university: De La Salle University - Manila
//--------------------------------------------------------------------------

namespace SerialSimulation.Models
{
    public class TracerData
    {
        private Person _name;
        public Person Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        private Activities _history;
        public Activities History
        {
            get
            {
                return _history;
            }
            set
            {
                _history = value;
            }
        }
    }
}
